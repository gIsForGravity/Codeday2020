using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "Untitled", menuName = "Game Map", order = 0)]
    public class Map : ScriptableObject
    {
        public Vector2 startingPoint;
        public Rotation startingPointRotation;
        public Vector2 endingPoint;
        public Rotation endingPointRotation;
        public Vector2[] mirrors;
        public Rotation[] mirrorRotations;
    }
}