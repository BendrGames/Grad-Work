using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Piece", menuName = "Piece")]
public class PieceData : ScriptableObject
{
    public string pieceName;

    public Sprite image;

    public int attack;
    public int health;
}

public enum PieceType
{
    Player,
    AIPiece
}
