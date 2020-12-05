using UnityEngine;

namespace Scripts
{
    public class MirrorComponent : MonoBehaviour, ICell
    {
        private void Awake()
        {
            ApplyRotation();
        }

        [SerializeField] private Rotation rotation = Rotation.Right;

        public void Rotate()
        {
            switch (rotation)
            {
                case Rotation.Left:
                    rotation = Rotation.Right;
                    break;
                case Rotation.Right:
                    rotation = Rotation.Left;
                    break;
            }

            ApplyRotation();
        }

        private void ApplyRotation()
        {
            switch (rotation)
            {
                case Rotation.Left:
                    transform.rotation = Quaternion.Euler(0, 0, -45);
                    break;
                case Rotation.Right:
                    transform.rotation = Quaternion.Euler(0, 0, 45);
                    break;
            }
        }

        public CellType Type => CellType.Mirror;
        public Rotation Rotation => rotation;
    }
}
