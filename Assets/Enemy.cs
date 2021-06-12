using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MoveableEntity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
    }

    // This gets called by MoveableEntity's Update method.
    // for AI movement you can call the DesireMoveX() methods, and MoveableEntity
    // will validate/move there in it's own update method.
    protected override void MovementCode()
    {
        
    }

    public void HandleHit()
    {
        // todo actually do something
        var render = GetComponent<SpriteRenderer>();
        if (render != null)
            render.enabled = false;
    }

    protected override void DashCode(Vector3 oldPos, Vector3 newPos)
    {
        // n/a
    }
}
