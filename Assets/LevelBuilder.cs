using System.Text.RegularExpressions;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public TextAsset level;
    public TileType[] Tiles;

    // Start is called before the first frame update
    void Start()
    {
        if (level == null) return;

        BuildLevel();
    }

    void BuildLevel()
    {
        string text = level.text;
        string[] lines = Regex.Split(text, "\n|\r\n");

        for (int x = 0; x < lines.Length; x++)
        {
            GameObject currentParent = new GameObject($"{x}");
            currentParent.transform.SetParent(transform);
            string currentLine = lines[x];
            for (int z = 0; z < currentLine.Length; z++)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.SetParent(currentParent.transform);
                go.transform.position = new Vector3(z, -1f, -x);
                go.name = $"{z}";

                Material m = GetMaterial(currentLine[z]);
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
