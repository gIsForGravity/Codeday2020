using System;
using UnityEngine;

namespace Scripts.Platformer
{
    public class PlatformingManager : MonoBehaviour
    {
        [SerializeField] private Vector2[] spawnLocations;
        
        private void Awake()
        {
            var player = Instantiate(Resources.Load<GameObject>("Player"));

            player.transform.position = spawnLocations[GameManager.Level];
        }
    }
}