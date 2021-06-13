using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData", order = 1)]
    public class TileManagerSO : ScriptableObject
    {
        public TileType[] tiles;

        public void LoadSprites()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].SpriteManager = new SpriteManager(tiles[i].Texture);
            }
        }
    }
}
