using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Levels
{
    public class LevelBuilder : MonoBehaviour
    {
        public static LevelBuilder current;

        [Tooltip("The text to generate the level from")]
        public TextAsset level;
        public TileManagerSO TileTypes;
        /// <summary>
        /// The tilemap created based on the data in <see cref="level"/>.
        /// </summary>
        public TileType[,] tileMap;


        /// <summary>
        /// The default tile.
        /// <para>Used as padding when the data in <see cref="level"/> is not perfectly rectangular.</para>
        /// </summary>
        private readonly TileType _nullTile = new TileType { Id = '!', BlockMovement = true };


        private void Awake()
        {
            if (level == null) return;
            if (current != null)
            {
                Destroy(this);
                return;
            }

            current = this;
        }

        void Start()
        {
            TileTypes.LoadSprites();
            BuildLevel();
        }

        void BuildLevel()
        {
            string text = level.text;
            string[] lines = Regex.Split(text, "\n|\r\n");

            CalcLevelBounds(lines);
            PopulateTilemap(lines);
            Build();
        }

        /// <summary>
        /// Calculates the bounds of the level based on the text in <paramref name="lines"/>,
        /// the instantiates <see cref="tileMap"/> to the correct size.
        /// </summary>
        /// <param name="lines"></param>
        private void CalcLevelBounds(string[] lines)
        {
            int xBound = lines.Length;
            int zBound = 0;
            for (int x = 0; x < lines.Length; x++)
            {
                int currentLength = lines[x].Length;
                if (currentLength > zBound)
                    zBound = currentLength;
            }
            tileMap = new TileType[xBound, zBound];
        }

        /// <summary>
        /// Populates the data of <see cref="tileMap"/> based on <paramref name="lines"/>.
        /// </summary>
        /// <param name="lines"></param>
        private void PopulateTilemap(string[] lines)
        {
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                string currentLine = lines[x];
                for (int z = 0; z < tileMap.GetLength(1); z++)
                {
                    tileMap[x, z] = (z >= currentLine.Length) ? _nullTile : GetTileType(currentLine[z]);
                }
            }
        }

        /// <summary>
        /// Iterates through <see cref="tileMap"/> and creates GameObjects based on the loaded values.
        /// </summary>
        private void Build()
        {
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                GameObject currentParent = new GameObject($"{x}");
                currentParent.transform.SetParent(transform);
                for (int z = 0; z < tileMap.GetLength(1); z++)
                {
                    TileType currentTile = tileMap[x, z];
                    if (currentTile == _nullTile) continue;

                    var go = new GameObject($"{z}");
                    go.transform.SetParent(currentParent.transform);
                    go.transform.position = new Vector3(x, -0.5f + currentTile.Height, z);
                    go.transform.rotation = Quaternion.Euler(90f, -90f, 0);

                    TileLocation tl = GetTileLocation(x, z, currentTile);

                    var spriteRenderer = go.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = currentTile.SpriteManager[tl];
                    //Material m = currentTile.Material;
                    //if (m != null)
                    //go.GetComponent<Renderer>().material = m;
                }
            }
        }

        private TileType GetTileType(char c)
        {
            foreach (TileType t in TileTypes.tiles)
            {
                if (t.Id == c)
                    return t;
            }
            return _nullTile;
        }

        private bool IsValidMove(Vector3Int position)
        {
            if (IsOutOfBounds(position.x, position.z))
                return false;
            return !tileMap[position.x, position.z].BlockMovement;
        }

        /// <summary>
        /// Checks that position is not out of bounds, and the block is allowed to be walked on
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsValidMove(Vector3 position)
        {
            //return IsValidMove(new Vector3Int((int)position.x, 0, (int)position.z));
            return IsValidMove(new Vector3Int(Mathf.CeilToInt(position.x), 0, Mathf.CeilToInt(position.z))) &&
                IsValidMove(new Vector3Int(Mathf.FloorToInt(position.x), 0, Mathf.FloorToInt(position.z)));
        }

        private bool IsOutOfBounds(int x, int z)
        {
            if (x < 0 || x >= tileMap.GetLength(0))
                return true;
            if (z < 0 || z >= tileMap.GetLength(1))
                return true;
            return false;
        }

        #region Texture stuff
        private bool IsSameTexture(int x, int z, TileType currentTile)
        {
            if (IsOutOfBounds(x, z))
                return false;
            return tileMap[x, z] == currentTile;
        }


        // todo rename tileLocation to something more fitting
        private TileLocation GetTileLocation(int x, int z, TileType currentTile)
        {
            // all 4 adjacent are good? then middle
            // bool up = IsValidMove(new Vector3Int(x - 1, 0, z));
            // bool down = IsValidMove(new Vector3Int(x + 1, 0, z));
            // bool left = IsValidMove(new Vector3Int(x, 0, z - 1));
            // bool right = IsValidMove(new Vector3Int(x, 0, z + 1));
            bool up = IsSameTexture(x - 1, z, currentTile);
            bool down = IsSameTexture(x + 1, z, currentTile);
            bool left = IsSameTexture(x, z - 1, currentTile);
            bool right = IsSameTexture(x, z + 1, currentTile);

            //   w   U   x
            //      ___
            //   L  | |  R
            //      ---
            //   y   D   z
            //



            // udlr
            // 0000 -> Square
            // 0001 -> HorizontalLeft
            // 0010 -> HorizontalRight
            // 0011 -> HorizontalMiddle
            // 0100 -> VerticalTop
            // 0101 -> TopLeft
            // 0110 -> TopRight
            // 0111 -> TopMiddle
            // 1000 -> VerticalBottom
            // 1001 -> BottomLeft
            // 1010 -> BottomRight
            // 1011 -> BottomMiddle
            // 1100 -> VerticalMiddle
            // 1101 -> MiddleLeft
            // 1110 -> MiddleRight
            // 1111 -> Middle

            int move = 0;
            move += (right) ? 1 : 0;
            move += ((left) ? 1 : 0) << 1;
            move += ((down) ? 1 : 0) << 2;
            move += ((up) ? 1 : 0) << 3;

            try
            {
                TileLocationFromAdjacent t = (TileLocationFromAdjacent)move;
                return t.Convert();
            }
            catch (Exception)
            {
                Debug.LogError($"Could not get tile location for {move}");
                return TileLocation.Middle;
            }
        }
        #endregion
    }

}