using System;
using UnityEngine;

namespace Scripts
{
    public class InputComponent : MonoBehaviour, ICell
    {
        public CellType Type => CellType.Input;

        public Rotation Rotation { get; set; } = Rotation.Up;

        public Transform Transform => transform;

        private Rotation lastRotation = Rotation.None;
        private void Update()
        {
            if (Rotation == lastRotation) return;
            lastRotation = Rotation;
            switch (Rotation)
            {
                case Rotation.Up:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Rotation.Right:
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case Rotation.Left:
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                case Rotation.Down:
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
            }
        }
    }
}