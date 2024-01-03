using Data;
using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
namespace Game
{
    public class Board : MonoBehaviour
    {
        public PieceView pieceprefab;

        [FormerlySerializedAs("Playerpieces")]
        public List<PieceView> PlayerPieces;
        [FormerlySerializedAs("AIPieces")]
        public List<PieceView> EnemyPieces;

        [Header("positioning")]
        public float middleSepperation;
        public float DistanceBetweenPieces;

        Vector3 BoardCenter = Vector3.zero;

        [SerializeField]
        private int piecestoSpawn = 5;

        [SerializeField]
        private PieceDataList pieceDataList;
        
        

        public List<PieceData> SelectPiecesToPlay()
        {
            if (pieceDataList == null)
            {
                throw new System.Exception("list not entered");
            }
            if (piecestoSpawn <= pieceDataList.pieceList.Count)
            {
                List<PieceData> temp = new();
                temp.AddRange(pieceDataList.pieceList);
                temp.Shuffle();

                return temp.Take(piecestoSpawn).ToList();
            }
            else
            {
                throw new System.Exception("Not enough pieces in the list");
            }
        }

        public void GenerateBoard()
        {
            List<PieceData> data = SelectPiecesToPlay();
            List<PieceData> playerdataSpawns = new();
            List<PieceData> AIdataSpawns = new();

            playerdataSpawns.AddRange(data);
            playerdataSpawns.Shuffle();
            AIdataSpawns.AddRange(data);
            AIdataSpawns.Shuffle();


            Vector3 playerStartPos = BoardCenter + new Vector3(0, 0, middleSepperation);
            Vector3 AIStartPos = BoardCenter + new Vector3(0, 0, -middleSepperation);

            int middleIndex = piecestoSpawn / 2; // Assuming piecestoSpawn is an odd number

            for (int i = 0; i < piecestoSpawn; i++)
            {
                int offsetFromMiddle = i - middleIndex;

                Vector3 playerPos = AIStartPos + new Vector3(-offsetFromMiddle * DistanceBetweenPieces, 0, 0);
                Vector3 AIpos = playerStartPos + new Vector3(offsetFromMiddle * DistanceBetweenPieces, 0, 0);

                PieceView playerPiece = Instantiate(pieceprefab, playerPos, Quaternion.identity);
                playerPiece.initializePiece(playerdataSpawns[i], PieceType.Player);

                PieceView AIPiece = Instantiate(pieceprefab, AIpos, Quaternion.identity);
                AIPiece.initializePiece(AIdataSpawns[i], PieceType.AIPiece);

                PlayerPieces.Add(playerPiece);
                EnemyPieces.Add(AIPiece);
            }
            
            PieceView closestBalancedPlayerPiece = null;
            int closestBalancedDifference = int.MaxValue;

            foreach (PieceView piece in PlayerPieces)
            {
                int difference = Math.Abs(piece.Attack - piece.Health);
                if (difference < closestBalancedDifference)
                {
                    closestBalancedDifference = difference;
                    closestBalancedPlayerPiece = piece;
                }
            }

            closestBalancedPlayerPiece.AddHealth(1);
            
        }

        public void DestroyBoard()
        {
            foreach (PieceView piece in PlayerPieces)
            {
                Destroy(piece.gameObject);
            }
            foreach (PieceView piece in EnemyPieces)
            {
                Destroy(piece.gameObject);
            }
            PlayerPieces.Clear();
            EnemyPieces.Clear();
        }

        public bool DidAnyPlayerLose()
        {
            if (PlayerPieces.Count == 0)
            {

                return true;
            }
            if (EnemyPieces.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void RemovePieceFromAlivePieces(PieceView currentarget)
        {
            if (currentarget.pieceType == PieceType.Player)
            {
                PlayerPieces.Remove(currentarget);
            }
            else if (currentarget.pieceType == PieceType.AIPiece)
            {
                EnemyPieces.Remove(currentarget);
            }
        }
        public void CheckForDeadPieces()
        {
            for (int index = PlayerPieces.Count - 1; index >= 0; index--)
            {
                PieceView piece = PlayerPieces[index];
                if (piece.Health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                    piece.PieceDeath();

                }
            }
            for (int index = EnemyPieces.Count - 1; index >= 0; index--)
            {
                PieceView piece = EnemyPieces[index];
                if (piece.Health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                    piece.PieceDeath();

                }
            }
        }
        public GameOutCome CheckWhoLost()
        {
            if (PlayerPieces.Count == 0 && EnemyPieces.Count == 0)
            {
                Gameloop.Instance.gameOutcome = GameOutCome.Draw;;
                return GameOutCome.Draw;
            }
            else if (PlayerPieces.Count == 0)
            {
                Gameloop.Instance.gameOutcome = GameOutCome.AIWon;;
                return GameOutCome.AIWon;
            }
            else
            {
                Gameloop.Instance.gameOutcome = GameOutCome.PlayerWon;;
                return GameOutCome.PlayerWon;
            }



        }

        public bool isPlayerPieceInFrontOfMe(PieceView AttackingPiece, out PieceView foundPlayerPiece)
        {
            float TOLERANCE = 0.2f;
            foreach (PieceView playerPiece in PlayerPieces)
            {
                if (Math.Abs(AttackingPiece.transform.position.x - playerPiece.transform.position.x) < TOLERANCE)
                {
                    foundPlayerPiece = playerPiece;
                    return true;
                }
            }
            foundPlayerPiece = null;
            return false;
        }

    }

    public enum GameOutCome { PlayerWon, AIWon, Draw }


}
