using UnityEngine;

namespace Scripts
{
    public class MirrorComponent : MonoBehaviour, ICell
    {
        private void Awake()
        {
            ApplyRotation();
        }

        public void Rotate()
        {
            Debug.Log("rotate");
            
            switch (Rotation)
            {
                case Rotation.DiagonalTopLeft:
                    Rotation = Rotation.DiagonalTopRight;
                    break;
                case Rotation.DiagonalTopRight:
                    Rotation = Rotation.DiagonalTopLeft;
                    break;
                default:
                    Rotation = Rotation.DiagonalTopRight;
                    break;
            }

            ApplyRotation();
        }

        private void ApplyRotation()
        {
            switch (Rotation)
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
        public Rotation Rotation { get; set; } = Rotation.DiagonalTopRight;
        public Transform Transform => transform;
    }
}
