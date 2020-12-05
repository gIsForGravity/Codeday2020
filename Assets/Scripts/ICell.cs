namespace Scripts
{
    public interface ICell
    {
        CellType Type { get; }
        Rotation Rotation { get; }
    }
}
