﻿using UnityEngine;
using System;

public class FirstPersonCamera : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine Methods
	//-----------------------------------------------------------------------------

	void Start ()
	{
		motor = GetComponent <PlayerMotor> ();
		cameraDistance = YDistanceBetween (middleBody.transform, _camera.transform);
		initialCameraRotationLimit = motor.CameraRotationLimit;
		initialCameraYPosition = CameraYPosition ();
		motor.RotateCamera (0f);
	}

	void Update ()
	{
		UpdateCameraHorizontalRotation ();
		UpdateCameraVerticalRotation ();
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void UpdateCameraHorizontalRotation ()
	{
		Vector3 rotation = new Vector3 (0f, MouseMovementVariation ().x, 0f) * lookSensibility;
		motor.Rotate (rotation);
	}

	void UpdateCameraVerticalRotation ()
	{
		motor.RotateCamera (MouseMovementVariation ().y * lookSensibility);
		motor.CameraHeight = YPosition (middleBody.transform) + cameraDistance;
		if (CameraYPosition () < initialCameraYPosition - 0.1f)
			motor.CameraRotationLimit = crouchCameraVerticalLimit;
		else
			motor.CameraRotationLimit = initialCameraRotationLimit;
	}

	Vector2 MouseMovementVariation ()
	{
		return Util.Input.MouseMovementDelta ();
	}

	float YPosition (Transform _transform)
	{
		return _transform.position.y;
	}

	float YDistanceBetween (Transform tA, Transform tB)
	{
		return Math.Abs (YPosition (tA) - YPosition (tB));
	}

	float CameraYPosition ()
	{
		return YPosition (_camera.transform);
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private float lookSensibility = 3f;

	[SerializeField]
	private float crouchCameraVerticalLimit = 40f;

	[SerializeField]
	private GameObject middleBody;

	[SerializeField]
	private Camera _camera;

	private PlayerMotor motor;

	private float cameraDistance, initialCameraYPosition, initialCameraRotationLimit;
}