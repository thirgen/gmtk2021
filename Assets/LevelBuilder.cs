using System.Text.RegularExpressions;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public static LevelBuilder current;

    public TextAsset level;
    public TileType[] Tiles;
    public char[,] tileMapo;
    public TileType NullTile = new TileType { Id = '!', BlockMovement = true, Material = null };
    public TileType[,] tileMap;
    private static readonly char NULL_CHAR = '!';


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

    private void PopulateTilemap(string[] lines)
    {
        for (int x = 0; x < tileMap.GetLength(0); x++)
        {
            string currentLine = lines[x];
            for (int z = 0; z < tileMap.GetLength(1); z++)
            {
                tileMap[x, z] = (z >= currentLine.Length) ? NullTile : GetTileType(currentLine[z]);
            }
        }
    }

    private void Build()
    {
        for (int x = 0; x < tileMap.GetLength(0); x++)
        {
            GameObject currentParent = new GameObject($"{x}");
            currentParent.transform.SetParent(transform);
            for (int z = 0; z < tileMap.GetLength(1); z++)
            {
                TileType currentTile = tileMap[x, z];
                if (currentTile.Id == NullTile.Id) continue;

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
        return NullTile;
    }

    private Material GetMaterial(char ch)
    {
        foreach(TileType t in Tiles)
        {
            if (t.Id == ch)
                return t.Material;
        }
        return null;
    }

    public bool IsValidMove(Vector3Int position)
    {
        if (position.x < 0 || position.x >= tileMap.GetLength(0))
            return false;
        if (position.z < 0 || position.z >= tileMap.GetLength(1))
            return false;
        return !tileMap[position.x, position.z].BlockMovement;
    }

    public bool IsValidMove(Vector3 position)
    {
        //return IsValidMove(new Vector3Int((int)position.x, 0, (int)position.z));
        return IsValidMove(new Vector3Int(Mathf.CeilToInt(position.x), 0, Mathf.CeilToInt(position.z))) &&
            IsValidMove(new Vector3Int(Mathf.FloorToInt(position.x), 0, Mathf.FloorToInt(position.z)));
    }
}
