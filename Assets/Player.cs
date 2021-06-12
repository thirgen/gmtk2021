using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MoveableEntity
{

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
    private EnemyManager _enemyManager;
    private Animator _animator;

    /// <summary>
    /// The time of the last successful dash.
    /// </summary>
    private float _lastDash = 0f;
    private bool _isDashing;

    private void Start()
    {
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

        #region Basic movement
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
        #endregion


        #region Dashing
        // Dash will move the player 1.5f along the z axis. No dashing up/down
        _isDashing = false;
        Vector3 zDash = Vector3.forward * 2f;
        if (Input.GetKeyDown(_rightDashKey) && CanDash)
        {
            desiredPosition += zDash;
            _lastDash = Time.time;
            _isDashing = true;
        }
        if (Input.GetKeyDown(_leftDashKey) && CanDash)
        {
            desiredPosition -= zDash;
            _lastDash = Time.time;
            _isDashing = true;
        }
        #endregion

        MoveToDesiredPosition();
    }

    protected override void DashCode(Vector3 oldPos, Vector3 newPos)
    {
        if (_isDashing)
        {
            // play dash animation
            _enemyManager.HandlePlayerDash(oldPos, newPos);
            _animator.SetTrigger("Dash");
        }
    }

    /// <summary>
    /// Returns true if it has been longer than <see cref="DashCooldown"/> since the last attempted dash
    /// </summary>
    private bool CanDash => Time.time - _lastDash > DashCooldown;
}
