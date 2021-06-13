using System;
using UnityEngine;

namespace Levels
{
    public class SpriteManager
    {
        private Sprite[] sprites;

        public SpriteManager(Texture2D texture)
        {
            sprites = Resources.LoadAll<Sprite>(texture.name);
        }

        public Sprite this[TileLocation location]
        {
            get
            {
                try
                {
                    return sprites[(int)location];
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.LogError($"Sprite {location} not found.");
                    return null;
                }
            }
        }

        public Sprite Default => this[TileLocation.Middle];

    }
}
