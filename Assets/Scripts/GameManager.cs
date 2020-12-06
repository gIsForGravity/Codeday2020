using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Vector2 topLeft;

        [SerializeField] private LineRenderer line;

        [SerializeField] private Map[] levels;

        private readonly ICell[,] _grid = new ICell[8,8];
        private Vector2 _startLocation;

        private int level = 0;
        private void Awake()
        {
            LoadMap(levels[level]);
        }

        private void LoadMap(Map map)
        {
            var outputComponent = Instantiate(Resources.Load<GameObject>("OutputWhite")).GetComponent<OutputComponent>();
            outputComponent.Rotation = map.startingPointRotation;
            _grid[(int) map.startingPoint.x, (int) map.startingPoint.y] = outputComponent;
            _startLocation = map.startingPoint;

            if (map.mirrors.Length != map.mirrorRotations.Length) throw new ArgumentException("mirrors and mirrorRotations have a different length.");

            for (int i = 0; i < map.mirrors.Length; i++)
            {
                var location = map.mirrors[i];
                var rotation = map.mirrorRotations[i];
                
                var mirror = Instantiate(Resources.Load<GameObject>("Mirror")).GetComponent<MirrorComponent>();

                _grid[(int) location.x, (int) location.y] = mirror;
                mirror.Rotation = rotation;
            }
            
            for (int iy = 0; iy < 8; iy++)
            {
                for (int ix = 0; ix < 8; ix++)
                    if (_grid[ix, iy] != null)
                        _grid[ix, iy].Transform.position = LocatePosition(ix, iy);
            }
        }

        private void Update()
        {
            var success = CalculateResult(out var path);
            var nodes = path.ToArray();
            line.positionCount = nodes.Length;
            line.SetPositions(nodes);
            line.positionCount += 1;
            

            if (!success) return;
            level += 1;
            if (level > levels.Length) throw new NotImplementedException();
        }

        private bool CalculateResult(out List<Vector3> path)
        {
            path = new List<Vector3>();

            Rotation currentRotation = _grid[(int) _startLocation.x, (int) _startLocation.y].Rotation;
            int x = (int) _startLocation.x;
            int y = (int) _startLocation.y;

            while (true)
            {
                Debug.Log("x: " + x + ", y: " + y);
                path.Add(LocatePosition(x, y));

                ICell cell;

                if ((cell = _grid[x, y]) != null)
                {
                    if (cell.Type == CellType.Mirror)
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
                    } else if (cell.Type == CellType.Input)
                        return true;
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

                if (x > 7 || x < 0 || y > 7 || y < 0) break;
            }

            return false;
        }

        private Vector2 LocatePosition(int x, int y) => new Vector2(x * 0.6f + topLeft.x, topLeft.y - y * 0.6f);
    }
}