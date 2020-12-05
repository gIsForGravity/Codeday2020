using UnityEngine;

namespace Scripts
{
    public class MirrorComponent : MonoBehaviour, ICell
    {
        private void Awake()
        {
            ApplyRotation();
        }

        [SerializeField] private Rotation rotation = Rotation.DiagonalTopRight;

        public void Rotate()
        {
            Debug.Log("rotate");
            
            switch (rotation)
            {
                case Rotation.DiagonalTopLeft:
                    rotation = Rotation.DiagonalTopRight;
                    break;
                case Rotation.DiagonalTopRight:
                    rotation = Rotation.DiagonalTopLeft;
                    break;
                default:
                    rotation = Rotation.DiagonalTopRight;
                    break;
            }

            ApplyRotation();
        }

        private void ApplyRotation()
        {
            switch (rotation)
            {
                case Rotation.DiagonalTopLeft:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Rotation.DiagonalTopRight:
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
            }
        }

        public CellType Type => CellType.Mirror;
        public Rotation Rotation => rotation;
        public Transform Transform => transform;
    }
}
