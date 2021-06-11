using UnityEngine;

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

    // todo make increments smallers for more accurate left/right movements
    void HandleInput()
    {
        //float movementAmount = Time.deltaTime * MovementSpeed;
        //Vector3 movement = Vector3.zero;
        Vector3 desiredPosition = transform.position;

        if (Input.GetKeyDown(_upKey))
        {
            desiredPosition -= Vector3.right;
        }
        if (Input.GetKeyDown(_downKey))
        {
            desiredPosition += Vector3.right;
        }
        if (Input.GetKeyDown(_leftKey))
        {
            desiredPosition -= Vector3.forward;
        }
        if (Input.GetKeyDown(_rightKey))
        {
            desiredPosition += Vector3.forward;
        }

        if (transform.position != desiredPosition && _levelBuilder.IsValidMove(desiredPosition))
        {
            transform.position = desiredPosition;
        }
        // TODO else play bump animation?
    }
}
