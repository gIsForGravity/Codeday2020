using System;
using UnityEngine;

namespace Scripts
{
    public class InputComponent : MonoBehaviour, ICell
    {
        public CellType Type => CellType.Input;

        public Rotation Rotation => Rotation.None;

        public Transform Transform => transform;
    }
}