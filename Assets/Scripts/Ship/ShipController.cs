using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
    LEFT,
    RIGHT
}

public class ShipController : MonoBehaviour
{
    [Header("Game objects")]
    public Transform Nose;
    public Transform Tail;

    [Header("Movements")]
    public float MinMoveSpeed = 0.5f;
    public float MaxMoveSpeed = 3f;
    public float MomentumDecaySpeed = 0.5f;
    public float MovementAcceleration = 2f;

    private float CurrentSpeed = 0f;

    [Header("Rotations")]
    public float RotationSpeed = 1f;
    public float RotationMomentumDecaySpeed = 0.5f;
    public float RotationAcceleration = 1f;

    private float CurrentRotation = 0f;
    private bool IsPressingRotation = false;
    private DIRECTION RotationDirection;

    [Header("Map bounds")]
    public float LeftRightMaxDistance = 20f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxMoveSpeed, Time.deltaTime * MovementAcceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MinMoveSpeed, Time.deltaTime * MovementAcceleration);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            CurrentRotation = Mathf.Lerp(CurrentRotation, -1f * RotationSpeed, Time.deltaTime * RotationAcceleration);
            IsPressingRotation = true;
            RotationDirection = DIRECTION.LEFT;
        }

        if (Input.GetKey(KeyCode.D))
        {
            CurrentRotation = Mathf.Lerp(CurrentRotation, 1f * RotationSpeed, Time.deltaTime * RotationAcceleration);
            IsPressingRotation = true;
            RotationDirection = DIRECTION.RIGHT;
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.D))
        {
            IsPressingRotation = false;
        }

        Move();
        ApplyBuoyancy();
        TiltBoat();
    }

    private void Move()
    {
        Vector3 newPos = transform.position;

        // Decay movement and rotation
        CurrentSpeed -= Time.deltaTime * MomentumDecaySpeed;
        if (!IsPressingRotation)
        {
            if (RotationDirection == DIRECTION.LEFT)
            {
                CurrentRotation = CurrentRotation > 0 ? 0 : CurrentRotation + Time.deltaTime * RotationMomentumDecaySpeed;
            }
            else
            {
                CurrentRotation = CurrentRotation < 0 ? 0 : CurrentRotation - Time.deltaTime * RotationMomentumDecaySpeed;
            }
        }

        if (CurrentSpeed <= MinMoveSpeed)
        {
            CurrentSpeed = MinMoveSpeed;
        }

        // Update position
        newPos = newPos + transform.forward * CurrentSpeed * Time.deltaTime;
        transform.position = newPos;

        // Update rotation
        transform.Rotate(new Vector3(0, CurrentRotation, 0));
    }

    private void ApplyBuoyancy()
    {
        // Calculate the new height according to sea level
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newPos.y = SeaCalculator.Instance.GetWorldHeight(transform.position);

        // Apply it
        transform.position = newPos;
    }

    private void TiltBoat()
    {
        // Retrieve current nose/tail position
        Vector3 noseNewPos = new Vector3(Nose.position.x, Nose.position.y, Nose.position.z);
        Vector3 tailNewPos = new Vector3(Tail.position.x, Tail.position.y, Tail.position.z);

        // Calculate the new height according to sea level
        noseNewPos.y = SeaCalculator.Instance.GetWorldHeight(Nose.position);
        tailNewPos.y = SeaCalculator.Instance.GetWorldHeight(Tail.position);

        Vector3 direction = noseNewPos - tailNewPos;

        transform.LookAt(transform.position + direction);
    }

    public float GetSpeedPercent()
    {
        return (CurrentSpeed - MinMoveSpeed) / (MaxMoveSpeed - MinMoveSpeed);
    }
}
