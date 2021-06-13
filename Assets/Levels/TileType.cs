using UnityEngine;

namespace Levels
{
    [System.Serializable]
    public struct TileType
    {
        public char Id;
        public string Name;
        public Texture2D Texture;
        public bool BlockMovement;
        public int Height;
        public SpriteManager SpriteManager;

        public static bool operator ==(TileType a, TileType b)
        {
            return a.Id == b.Id;
        }

        public static bool operator !=(TileType a, TileType b)
        {
            return a.Id != b.Id;
        }

    }
}
