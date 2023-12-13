using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    [CreateAssetMenu(fileName = "New Piece", menuName = "PieceList")]
    
    public class PieceDataList : ScriptableObject
    {
       public List<PieceData> pieceList;
    }
}
