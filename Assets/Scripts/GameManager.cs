using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Vector2 topLeft;

        [SerializeField] private LineRenderer line;

        private readonly ICell[,] _grid = new ICell[8,8];
        private Vector2 _startLocation;
        
        private void Awake()
        {
            _grid[0, 3] = Instantiate(Resources.Load<GameObject>("Mirror")).GetComponent<MirrorComponent>();
            _grid[7, 3] = Instantiate(Resources.Load<GameObject>("Mirror")).GetComponent<MirrorComponent>();
            //_grid[5, 2] = Instantiate(Resources.Load<GameObject>("Mirror")).GetComponent<MirrorComponent>();
            var outputComponent = Instantiate(Resources.Load<GameObject>("OutputWhite")).GetComponent<OutputComponent>();
            outputComponent.Rotation = Rotation.Up;
            _grid[7, 7] = outputComponent;
            _startLocation = new Vector2(7, 7);

            for (int iy = 0; iy < 8; iy++)
            {
                for (int ix = 0; ix < 8; ix++)
                    if (_grid[ix, iy] != null)
                        _grid[ix, iy].Transform.position = LocatePosition(ix, iy);
            }
        }

        private void Update()
        {
            CalculateResult(out var path);
            var nodes = path.ToArray();
            line.positionCount = nodes.Length;
            line.SetPositions(nodes);
        }

        private void CalculateResult(out List<Vector3> path)
        {
            path = new List<Vector3>();

            Rotation currentRotation = _grid[(int) _startLocation.x, (int) _startLocation.y].Rotation;
            int x = (int) _startLocation.x;
            int y = (int) _startLocation.y;

            while (true)
            {
                path.Add(LocatePosition(x, y));

                ICell cell;

                if ((cell = _grid[x, y]).Type == CellType.Mirror)
                {
                    switch (cell.Rotation)
                    {
                        case Rotation.DiagonalTopRight:
                            switch (currentRotation)
                            {
                                case Rotation.Up:
                                    currentRotation = Rotation.Right;
                                    break;
                                case Rotation.Left:
                                    currentRotation = Rotation.Down;
                                    break;
                                case Rotation.Right:
                                    currentRotation = Rotation.Up;
                                    break;
                                case Rotation.Down:
                                    currentRotation = Rotation.Left;
                                    break;
                            }
                            break;
                        case Rotation.DiagonalTopLeft:
                            switch (currentRotation)
                            {
                                case Rotation.Up:
                                    currentRotation = Rotation.Left;
                                    break;
                                case Rotation.Left:
                                    currentRotation = Rotation.Up;
                                    break;
                                case Rotation.Right:
                                    currentRotation = Rotation.Down;
                                    break;
                                case Rotation.Down:
                                    currentRotation = Rotation.Right;
                                    break;
                            }
                            break;
                    }
                }

                switch (currentRotation)
                {
                    case Rotation.Up:
                        y -= 1;
                        break;
                    case Rotation.Left:
                        x -= 1;
                        break;
                    case Rotation.Right:
                        x += 1;
                        break;
                    case Rotation.Down:
                        y += 1;
                        break;
                }

                if (x > 8 || x < -1 || y > 8 || y < -1) return;
            }
        }

        private Vector2 LocatePosition(int x, int y) => new Vector2(x * 0.6f + topLeft.x, topLeft.y - y * 0.6f);
    }
}