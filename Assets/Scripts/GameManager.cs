using System;
using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Vector2 topLeft;

        private ICell[,] _grid = new ICell[8,8];
        
        private void Awake()
        {
            _grid[0, 0] = Instantiate(Resources.Load<GameObject>("Mirror")).GetComponent<MirrorComponent>();
            _grid[7, 3] = Instantiate(Resources.Load<GameObject>("Mirror")).GetComponent<MirrorComponent>();
            _grid[5, 2] = Instantiate(Resources.Load<GameObject>("Mirror")).GetComponent<MirrorComponent>();
            _grid[3, 7] = Instantiate(Resources.Load<GameObject>("OutputWhite")).GetComponent<OutputComponent>();

            for (int iy = 0; iy < 8; iy++)
            {
                for (int ix = 0; ix < 8; ix++)
                    if (_grid[ix, iy] != null)
                        _grid[ix, iy].Transform.position = LocatePosition(ix, iy);
            }
        }

        private Vector2 LocatePosition(int x, int y) => new Vector2(x * 0.6f + topLeft.x, topLeft.y - y * 0.6f);
    }
}