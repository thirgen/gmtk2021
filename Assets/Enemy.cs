using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleHit()
    {
        var render = GetComponent<SpriteRenderer>();
        if (render != null)
            render.enabled = false;
    }
}
