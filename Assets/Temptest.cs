using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temptest : MonoBehaviour
{
    public Texture2D texture;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        if (texture == null) return;

        sprites = Resources.LoadAll<Sprite>(texture.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
