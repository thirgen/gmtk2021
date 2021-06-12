using UnityEngine;

public class Player : MonoBehaviour
{
    public float ZDelay = 0.025f;

    private KeyCode _upKey = KeyCode.W;
    private KeyCode _downKey = KeyCode.S;
    private KeyCode _leftKey = KeyCode.A;
    private KeyCode _rightKey = KeyCode.D;
    private LevelBuilder _levelBuilder;
    private float _lastZMove = 0f;

    void Start()
    {
        _levelBuilder = LevelBuilder.current;
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector3 desiredPosition = transform.position;

        // todo ability to hold down key for up/down movements?
        if (Input.GetKeyDown(_upKey))
        {
            desiredPosition -= Vector3.right;
        }
        if (Input.GetKeyDown(_downKey))
        {
            desiredPosition += Vector3.right;
        }


        // Moves along the Z axis (left/right) in increments of 0.25f
        Vector3 zMovement = Vector3.forward / 4f;
        if (Input.GetKey(_rightKey) && CanMoveZ)
        {
            desiredPosition += zMovement;
        }
        if (Input.GetKey(_leftKey) && CanMoveZ)
        {
            desiredPosition -= zMovement;
        }

        if (transform.position != desiredPosition && _levelBuilder.IsValidMove(desiredPosition))
        {
            transform.position = desiredPosition;
            _lastZMove = Time.time;
        }
        // TODO else play bump animation?
    }

    /// <summary>
    /// Returns true if it has been longer than ZDelay since the last attempted move along the Z axis
    /// </summary>
    private bool CanMoveZ => Time.time - _lastZMove > ZDelay;
}
