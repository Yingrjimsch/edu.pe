using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class CubeRopeHandler : MonoBehaviour
{

    private readonly float[] ropePositions = new float[2] { -10f, -15.5f };
    public Cube cube;
    public Rigidbody cube2;
    public Rigidbody cube1; 

    private readonly float ropeLength = 6f;
    private readonly float gravity = 9.81f;

    private readonly float Cw = 1.1f;
    private readonly float rhoAir = 1.2f;

    // FixedUpdate can be called multiple times per frame
    private void FixedUpdate()
	{
		if (cube.GetState() != State.Swinging && cube1.position.x <= ropePositions[0])
		{
			Debug.Log("Start Swinging");
			Debug.Log("Cube1 Mass" + cube1.mass);
			cube.SetState(State.Swinging);
		}

		if (cube.GetState() != State.Swinging) return;

		// calculate Alpha
		float alpha = CalcAlpha();

		// Add force to Cube 1
		AddForceToRope(alpha, cube1);
		AddViscousFriction(cube1);

		// Add force to Cube 2
		AddForceToRope(alpha, cube2);
		AddViscousFriction(cube2);
	}

	private float CalcAlpha()
	{
		Vector3 ropeHookPoint = new(ropePositions[0], ropeLength, 0f);
		Vector3 diffVector = ropeHookPoint - cube1.position;
		return Mathf.Atan2(diffVector.x, diffVector.y); ;
	}

	private void AddForceToRope(float alpha, Rigidbody cube)
    {
        // Radialer Anteil der Gewichtskraft Fg = m * g * cos(alpha)
        double forceG = cube.mass * gravity * Math.Cos(alpha);
        // Zentripetalkraft Fz = m * v**2 / R
        double forceZ = cube.mass * Math.Pow(cube.velocity.magnitude, 2) / ropeLength;

        double forceX = (forceG + forceZ) * Math.Sin(alpha);
        double forceY = (forceG + forceZ) * Math.Cos(alpha);

        // finally add force
        cube.AddForce(new Vector3(Convert.ToSingle(forceX), Convert.ToSingle(forceY), 0f));
    }

    private void AddViscousFriction(Rigidbody cube)
    {
        float velocityX = cube.velocity.x;
                
        Vector3 velocityVec = cube.velocity.normalized;

        cube.AddForce(-0.5f * this.cube.GetSquare() * rhoAir * Cw * velocityX * velocityX * velocityVec);
    }


}
