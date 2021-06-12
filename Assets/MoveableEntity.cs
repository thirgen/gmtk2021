using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableEntity : MonoBehaviour
{
    /// <summary>
    /// Time in seconds in between movements along the Z axis (left/right).
    /// </summary>
    [Tooltip("Time in seconds in between movements along the Z axis (left/right).")]
    public float ZDelay = 0.025f;
    protected Vector3 desiredPosition = Vector3.zero;

    // Moves along the Z axis (left/right) in increments of 0.25f
    private Vector3 _zMovementAmount = Vector3.forward / 4f;
    private LevelBuilder _levelBuilder;


    /// <summary>
    /// The time of the last successful movement along the Z axis.
    /// </summary>
    protected float _lastZMove = 0f;
    /// <summary>
    /// Returns true if it has been longer than <see cref="ZDelay"/> since the last successful move along the Z axis
    /// </summary>
    protected bool CanMoveZ => Time.time - _lastZMove > ZDelay;



    protected virtual void Awake()
    {
        _levelBuilder = LevelBuilder.current;
    }

    protected virtual void Update()
    {
        desiredPosition = transform.position;
        MovementCode();
        MoveToDesiredPosition();
    }


    #region DesireMoveDirections
    protected void DesireMoveUp() => desiredPosition -= Vector3.right;
    protected void DesireMoveDown() => desiredPosition += Vector3.right;

    protected void DesireMoveRight()
    {
        if (CanMoveZ)
        {
            desiredPosition += _zMovementAmount;
        }
    }

    protected void DesireMoveLeft()
    {
        if (CanMoveZ)
        {
            desiredPosition -= _zMovementAmount;
        }
    }
    #endregion


    /// <summary>
    /// Check to see if <see cref="desiredPosition"/> if it is a valid position.
    /// </summary>
    protected void MoveToDesiredPosition()
    {
        if (transform.position != desiredPosition)
        {
            if (_levelBuilder.IsValidMove(desiredPosition))
            {
                Vector3 oldPos = transform.position;
                transform.position = desiredPosition;
                _lastZMove = Time.time;
                DashCode(oldPos, desiredPosition);
            }
            // TODO else play bump animation?
        }
    }

    #region Abstract methods
    protected abstract void MovementCode();

    /// <summary>
    /// Overwritten by Player. this is just here so we don't have to duplicate <see cref="MoveToDesiredPosition"/>
    /// </summary>
    /// <param name="oldPos"></param>
    /// <param name="newPos"></param>
    protected abstract void DashCode(Vector3 oldPos, Vector3 newPos);
    #endregion
}
