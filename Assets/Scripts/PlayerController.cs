﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

	[SerializeField] float _moveSpeed = 70;
	[SerializeField] float _movementSmoothing = 0.1f;
	[SerializeField] bool _normalizedMovement = true;
	[SerializeField] GameObject _downObject;

	private float _speed;
	private Vector2 _axisVector = Vector2.zero;
	private Vector3 _currentVelocity = Vector3.zero;

	Rigidbody2D _rb2D;
	Animator _currentAnimator;
	Animator downAnimator;

	void Start()
	{
		_rb2D = GetComponent<Rigidbody2D>();
		_downObject.SetActive(true);
		downAnimator = _downObject.GetComponent<Animator>();
		_currentAnimator = downAnimator;
	}

	void Update()
	{
		// get speed from the rigid body to be used for animator parameter Speed
		_speed = _rb2D.velocity.magnitude;

		// Get input axises
		_axisVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		//normalize it for good topdown diagonal movement
		if (_normalizedMovement == true)
		{
			_axisVector.Normalize();
		}

		// Set speed parameter to the animator
		_currentAnimator.SetFloat("Speed", _speed);

		// Check keys for actions and use appropiate function
		//
		if (Input.GetKey(KeyCode.Space))  // SWING ATTACK
		{
			PlayAnimation("Swing");
		}

		else if (Input.GetKey(KeyCode.C))  // THRUST ATTACK
		{
			PlayAnimation("Thrust");
		}

		else if (Input.GetKey(KeyCode.X))  // BOW ATTACK
		{
			PlayAnimation("Bow");
		}

	}

	void FixedUpdate()
	{
		// Move our character
		Move();
	}

	void PlayAnimation(string animationName)
	{
		// Play given animation in the current directions animator
		_currentAnimator.Play(animationName, 0);
	}


	void Move()
	{
		// Set target velocity to smooth towards
		Vector2 targetVelocity = new Vector2(_axisVector.x * _moveSpeed * 10f, _axisVector.y * _moveSpeed * 10) * Time.fixedDeltaTime;

		// Smoothing out the movement
		_rb2D.velocity = Vector3.SmoothDamp(_rb2D.velocity, targetVelocity, ref _currentVelocity, _movementSmoothing);
	}
}