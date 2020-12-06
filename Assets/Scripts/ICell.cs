using UnityEngine;

namespace Scripts
{
    public interface ICell
    {
        CellType Type { get; }
        Rotation Rotation { get; }
        Transform Transform { get; }
    }
}
