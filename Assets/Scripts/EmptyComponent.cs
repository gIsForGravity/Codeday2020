using UnityEngine;

namespace Scripts
{
    public class EmptyComponent : MonoBehaviour, ICell
    {
        public CellType Type => CellType.Empty;
    }
}