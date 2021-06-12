using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float XYDelay = 0.2f;

    private KeyCode _upKey = KeyCode.W;
    private KeyCode _downKey = KeyCode.S;
    private KeyCode _leftKey = KeyCode.A;
    private KeyCode _rightKey = KeyCode.D;
    private CharacterController _cc;
    private LevelBuilder _levelBuilder;
    private float _lastXYMove = 0f;

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
        float movementAmount = Time.deltaTime * MoveSpeed;
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

        int desiredZ = Mathf.RoundToInt(desiredPosition.z);
        if (Input.GetKey(_rightKey) && CanMoveXY)
        {
            // if moving right, round up
            desiredPosition += Vector3.forward / 4f;
            _lastXYMove = Time.time;
        }
        else if (Input.GetKey(_leftKey) && CanMoveXY)
        {
            // if moving left, round down
            desiredPosition -= Vector3.forward / 4f;
            _lastXYMove = Time.time;
        }

        Vector3Int temp = new Vector3Int((int)desiredPosition.x, 0, desiredZ);
        if (transform.position != desiredPosition && _levelBuilder.IsValidMove(desiredPosition))
        {
            Debug.Log($"{desiredZ}, {desiredPosition}, {Mathf.RoundToInt(desiredPosition.z)},   {temp}");
            //desiredPosition.z = desiredZ;
            transform.position = desiredPosition;
            
        }
        // TODO else play bump animation?
    }

    private bool CanMoveXY => Time.time - _lastXYMove > XYDelay;

    private void x()
    {
        /*
        int desiredZ = Mathf.RoundToInt(desiredPosition.z);
        if (Input.GetKey(_rightKey))
        {
            // if moving right, round up
            desiredPosition += Vector3.forward * movementAmount;
            //desiredPosition += (Vector3.forward / 2f) * movementAmount;
            desiredZ = Mathf.CeilToInt(desiredPosition.z);
            //desiredZ += Vector3.forward.z * movementAmount;
        }
        else if (Input.GetKey(_leftKey))
        {
            // if moving left, round down
            desiredPosition -= Vector3.forward * movementAmount;
            //desiredPosition -= (Vector3.forward / 2f) * movementAmount;
            desiredZ = Mathf.FloorToInt(desiredPosition.z);
            //desiredZ -= Vector3.forward.z * movementAmount;
        }

        Vector3Int temp = new Vector3Int((int)desiredPosition.x, 0, desiredZ);
        if (transform.position != desiredPosition && _levelBuilder.IsValidMove(temp))
        {
            Debug.Log($"{desiredZ}, {desiredPosition}, {Mathf.RoundToInt(desiredPosition.z)},   {temp}");
            //desiredPosition.z = desiredZ;
            transform.position = desiredPosition;

        }
        */
    }
}
