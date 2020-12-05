using UnityEngine;

namespace Scripts
{
    public class BlockComponent : MonoBehaviour, ICell
    {
        public CellType Type => CellType.Block;
    }
}