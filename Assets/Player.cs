using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Time in seconds in between movements along the Z axis (left/right).
    /// </summary>
    [Tooltip("Time in seconds in between movements along the Z axis (left/right).")]
    public float ZDelay = 0.025f;

    /// <summary>
    /// The minimum time in seconds in between dash attacks.
    /// </summary>
    [Tooltip("The minimum time in seconds in between dash attacks.")]
    public float DashCooldown = 0.25f;

    private KeyCode _upKey = KeyCode.W;
    private KeyCode _downKey = KeyCode.S;
    private KeyCode _leftKey = KeyCode.A;
    private KeyCode _rightKey = KeyCode.D;
    private KeyCode _upDashKey = KeyCode.UpArrow;
    private KeyCode _downDashKey = KeyCode.DownArrow;
    private KeyCode _leftDashKey = KeyCode.LeftArrow;
    private KeyCode _rightDashKey = KeyCode.RightArrow;
    private LevelBuilder _levelBuilder;
    private Animator _animator;

    /// <summary>
    /// The time of the last successful movement along the Z axis.
    /// </summary>
    private float _lastZMove = 0f;

    /// <summary>
    /// The time of the last successful dash.
    /// </summary>
    private float _lastDash = 0f;

    void Start()
    {
        _levelBuilder = LevelBuilder.current;
        _animator = GetComponent<Animator>();
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


        // Dash will move the player 1.5f along the z axis. No dashing up/down
        bool isDashing = false;
        Vector3 zDash = Vector3.forward * 2f;
        if (Input.GetKeyDown(_rightDashKey) && CanDash)
        {
            desiredPosition += zDash;
            _lastDash = Time.time;
            isDashing = true;
        }
        if (Input.GetKeyDown(_leftDashKey) && CanDash)
        {
            desiredPosition -= zDash;
            _lastDash = Time.time;
            isDashing = true;
        }

        if (transform.position != desiredPosition)
        {
            if (_levelBuilder.IsValidMove(desiredPosition))
            {
                transform.position = desiredPosition;
                _lastZMove = Time.time;
                if (isDashing)
                {
                    // play dash animation
                    _animator.SetTrigger("Dash");
                }
            }
            // TODO else play bump animation?
        }
    }

    /// <summary>
    /// Returns true if it has been longer than <see cref="ZDelay"/> since the last successful move along the Z axis
    /// </summary>
    private bool CanMoveZ => Time.time - _lastZMove > ZDelay;

    /// <summary>
    /// Returns true if it has been longer than <see cref="DashCooldown"/> since the last attempted dash
    /// </summary>
    private bool CanDash => Time.time - _lastDash > DashCooldown;
}
