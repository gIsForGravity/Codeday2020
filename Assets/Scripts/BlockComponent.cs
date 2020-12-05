using UnityEngine;

namespace Scripts
{
    public class BlockComponent : MonoBehaviour, ICell
    {
        public CellType Type => CellType.Block;

        public Rotation Rotation => Rotation.None;

        public Transform Transform => transform;
    }
}