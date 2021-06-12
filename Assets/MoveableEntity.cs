using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableEntity : MonoBehaviour
{
    protected Vector3 desiredPosition = Vector3.zero;
    // Moves along the Z axis (left/right) in increments of 0.25f
    private Vector3 _zMovement = Vector3.forward / 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void DesireMoveUp() => desiredPosition -= Vector3.right;
    protected void DesireMoveDown() => desiredPosition += Vector3.right;
    protected void DesireMoveRight() => desiredPosition += _zMovement;
    protected void DesireMoveLeft() => desiredPosition -= _zMovement;


}
