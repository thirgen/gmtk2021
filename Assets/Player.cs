using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MoveableEntity
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
    private EnemyManager _enemyManager;
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
        _enemyManager = EnemyManager.current;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        desiredPosition = transform.position;

        // todo ability to hold down key for up/down movements?
        if (Input.GetKeyDown(_upKey))
        {
            DesireMoveUp();
        }
        if (Input.GetKeyDown(_downKey))
        {
            DesireMoveDown();
        }

        if (Input.GetKey(_rightKey) && CanMoveZ)
        {
            DesireMoveRight();
        }
        if (Input.GetKey(_leftKey) && CanMoveZ)
        {

            DesireMoveLeft();
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
                Vector3 oldPos = transform.position;
                transform.position = desiredPosition;
                _lastZMove = Time.time;
                if (isDashing)
                {
                    // play dash animation
                    _enemyManager.HandlePlayerDash(oldPos, desiredPosition);
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
