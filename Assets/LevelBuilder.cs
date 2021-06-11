using System.Text.RegularExpressions;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public TextAsset level;
    public TileType[] Tiles;
    public char[,] tileMap;
    private static readonly char NULL_CHAR = '!';

    void Start()
    {
        if (level == null) return;

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
        tileMap = new char[xBound, zBound];
    }

    private void PopulateTilemap(string[] lines)
    {
        for (int x = 0; x < tileMap.GetLength(0); x++)
        {
            string currentLine = lines[x];
            for (int z = 0; z < tileMap.GetLength(1); z++)
            {
                tileMap[x, z] = (z >= currentLine.Length) ? NULL_CHAR : currentLine[z];
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
                char currentChar = tileMap[x, z];
                if (currentChar == NULL_CHAR) continue;

                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.SetParent(currentParent.transform);
                go.transform.position = new Vector3(z, -1f, -x);
                go.name = $"{z}";

                Material m = GetMaterial(currentChar);
                if (m != null)
                    go.GetComponent<Renderer>().material = m;
            }
        }
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
}
