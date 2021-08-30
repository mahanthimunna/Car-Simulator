using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carcontroller : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentsteerangle;
    private bool isBreaking;
    private float currentbreakForce;

    [SerializeField] private float MotorForce;
    [SerializeField] private float BreakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransorm;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    private void fixedupdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * MotorForce;
        frontRightWheelCollider.motorTorque = verticalInput * MotorForce;
        currentbreakForce = isBreaking ? BreakForce : 0f;
        if(isBreaking)
        {
            ApplyBreaking();
        }
    }
    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque=currentbreakForce;
        frontRightWheelCollider.brakeTorque=currentbreakForce;
        rearLeftWheelCollider.brakeTorque=currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }
    private void HandleSteering()
    {
        currentsteerangle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentsteerangle;
        frontRightWheelCollider.steerAngle = currentsteerangle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransorm);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider,Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}