using UnityEngine;

namespace Scripts
{
    public class EmptyComponent : MonoBehaviour, ICell
    {
        public CellType Type => CellType.Empty;

        public Rotation Rotation => Rotation.None;
    }
}