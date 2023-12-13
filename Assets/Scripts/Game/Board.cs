using Data;
using DefaultNamespace;
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
        public List<PieceView> AIPieces;
        
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
                List<PieceData> temp = new List<PieceData>();
                temp.AddRange(pieceDataList.pieceList);
                temp.Shuffle();

                return temp.Take(5).ToList();
            }
            else
            {
                throw new System.Exception("Not enough pieces in the list");
            }
        }

        public void GenerateBoard()
        {
            List<PieceData> data = SelectPiecesToPlay();
            List<PieceData> playerdataSpawns = new List<PieceData>();
            List<PieceData> AIdataSpawns = new List<PieceData>();
            
            playerdataSpawns.AddRange(data);
            playerdataSpawns.Shuffle();
            AIdataSpawns.AddRange(data);
            AIdataSpawns.Shuffle();

            
            Vector3 playerStartPos = BoardCenter + new Vector3(0, 0, middleSepperation);
            Vector3 AIStartPos = BoardCenter + new Vector3(0, 0, -middleSepperation);
            
            int middleIndex = piecestoSpawn / 2;  // Assuming piecestoSpawn is an odd number

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
                AIPieces.Add(AIPiece);
            }
        }
        
        public void DestroyBoard()
        {
            foreach (PieceView piece in PlayerPieces)
            {
                Destroy(piece.gameObject);
            }
            foreach (PieceView piece in AIPieces)
            {
                Destroy(piece.gameObject);
            }
            PlayerPieces.Clear();
            AIPieces.Clear();
        }

        public bool DidAnyPlayerLose()
        {
            if (PlayerPieces.Count == 0)
            {
                Gameloop.Instance.wonLastCombat = false;
                return true;
            }
             if (AIPieces.Count == 0)
            {
                Gameloop.Instance.wonLastCombat = true;
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
                AIPieces.Remove(currentarget);
            }
        }
        public void CheckForDeadPieces()
        {
            for (int index = PlayerPieces.Count - 1; index >= 0; index--)
            {
                PieceView piece = PlayerPieces[index];
                if (piece.health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                    piece.PieceDeath();
                    
                }
            }
            for (int index = AIPieces.Count - 1; index >= 0; index--)
            {
                PieceView piece = AIPieces[index];
                if (piece.health <= 0)
                {
                    RemovePieceFromAlivePieces(piece);
                    piece.PieceDeath();
                  
                }
            }
        }
    }
    
    
}