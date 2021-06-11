using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    public float MovementSpeed = 1000f;
    private KeyCode _upKey = KeyCode.W;
    private KeyCode _downKey = KeyCode.S;
    private KeyCode _leftKey = KeyCode.A;
    private KeyCode _rightKey = KeyCode.D;
    private CharacterController _cc;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        //float movementAmount = Time.deltaTime * MovementSpeed;
        //Vector3 movement = Vector3.zero;

        if (Input.GetKeyDown(_upKey))
        {
            transform.position += transform.forward;
            //movement += transform.forward;
        }
        if (Input.GetKeyDown(_downKey))
        {
            transform.position -= transform.forward;
            //movement -= transform.forward;
        }
        if (Input.GetKeyDown(_leftKey))
        {
            transform.position -= transform.right;
            //movement -= transform.right;
        }
        if (Input.GetKeyDown(_rightKey))
        {
            transform.position += transform.right;
            //movement += transform.right;
        }

        //_cc.SimpleMove(movement * movementAmount);
    }
}
