using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 10f;

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

        int desiredZ = 0;
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

        if (transform.position != desiredPosition && _levelBuilder.IsValidMove(new Vector3Int((int)desiredPosition.x, 0, desiredZ)))
        {
            Debug.Log($"{desiredZ}, {desiredPosition}, {Mathf.RoundToInt(desiredPosition.z)}");
            //desiredPosition.z = desiredZ;
            transform.position = desiredPosition;
            
        }
        // TODO else play bump animation?
    }
}
