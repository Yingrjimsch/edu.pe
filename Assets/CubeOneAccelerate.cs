using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.IO;

public class CubeOneAccelerate : MonoBehaviour
{
	private Rigidbody rigidBody;

	public float maxSpeed = 2;
	public Cube cube;
	public float forceX = 1;

	// Start is called before the first frame update
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	// FixedUpdate can be called multiple times per frame
	void FixedUpdate()
	{
		if (cube.GetState() != State.Accelerate) return;
		if (MaxSpeedReached())
		{
			Debug.Log("Start Compression");
			cube.SetState(State.Compress);
		}
		else
		{
			cube.SetForceX(forceX);
		}
	}

	private bool MaxSpeedReached()
	{
		return Math.Abs(cube.GetVelocity()) >= maxSpeed;
	}
}
