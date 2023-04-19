using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOneCompress : MonoBehaviour
{
	public float springConstant = 2f;
	public Cube cube;

	private float startCompressionX;
	private bool init;

	// Start is called before the first frame update
	void Start()
	{
		startCompressionX = float.MinValue;
		init = true;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		if (cube.GetState() != State.Compress) return;
		if (startCompressionX == float.MinValue)
		{
			Debug.Log("Start Compression Position:" + startCompressionX);
			startCompressionX = cube.GetPositionX();
		}
		if (IsOnStartPosition() && !init)
		{
			cube.SetForceX();
			cube.SetState(State.FreeFloating);
		}
		else
		{
			init = false;
			float deflection = Math.Abs(cube.GetPositionX() - startCompressionX);
			float springForce = -springConstant * deflection;
			Debug.Log("Compressing with deflection: " + deflection + " and springForce: " + springForce);
			cube.SetForceX(springForce);
		}
	}

	private bool IsOnStartPosition()
	{
		return cube.GetPositionX() <= startCompressionX;
	}
}
