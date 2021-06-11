using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    private KeyCode _upKey = KeyCode.W;
    private KeyCode _downKey = KeyCode.S;
    private KeyCode _leftKey = KeyCode.A;
    private KeyCode _rightKey = KeyCode.D;
    private CharacterController _cc;
    private LevelBuilder _levelBuilder;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _levelBuilder = LevelBuilder.current;
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        //float movementAmount = Time.deltaTime * MovementSpeed;
        //Vector3 movement = Vector3.zero;
        Vector3 desiredPosition = transform.position;

        if (Input.GetKeyDown(_upKey))
        {
            desiredPosition += transform.forward;
        }
        if (Input.GetKeyDown(_downKey))
        {
            desiredPosition -= transform.forward;
        }
        if (Input.GetKeyDown(_leftKey))
        {
            desiredPosition -= transform.right;
        }
        if (Input.GetKeyDown(_rightKey))
        {
            desiredPosition += transform.right;
        }

        if (transform.position != desiredPosition && _levelBuilder.IsValidMove(desiredPosition))
        {
            transform.position = desiredPosition;
        }
        // TODO else play bump animation?
    }
}
