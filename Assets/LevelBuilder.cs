using System.Text.RegularExpressions;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public static LevelBuilder current;

    [Tooltip("The text to generate the level from")]
    public TextAsset level;
    public TileType[] Tiles;
    /// <summary>
    /// The tilemap created based on the data in <see cref="level"/>.
    /// </summary>
    public TileType[,] tileMap;


    /// <summary>
    /// The default tile.
    /// <para>Used as padding when the data in <see cref="level"/> is not perfectly rectangular.</para>
    /// </summary>
    private readonly TileType _nullTile = new TileType { Id = '!', BlockMovement = true, Material = null };


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
                if (currentTile.Id == _nullTile.Id) continue;

                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.SetParent(currentParent.transform);
                go.transform.position = new Vector3(x, -1f + currentTile.Height, z);
                go.name = $"{z}";

                Material m = currentTile.Material;
                if (m != null)
                    go.GetComponent<Renderer>().material = m;
            }
        }
    }

    private TileType GetTileType(char c)
    {
        foreach (TileType t in Tiles)
        {
            if (t.Id == c)
                return t;
        }
        return _nullTile;
    }

    private bool IsValidMove(Vector3Int position)
    {
        if (position.x < 0 || position.x >= tileMap.GetLength(0))
            return false;
        if (position.z < 0 || position.z >= tileMap.GetLength(1))
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
}
