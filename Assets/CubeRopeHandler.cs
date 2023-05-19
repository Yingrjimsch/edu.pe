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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate can be called multiple times per frame
    private void FixedUpdate() 
    {
        if (cube1.position.x <= ropePositions[0]) {
            cube.SetState(State.Swinging);
        }

        if (cube.GetState() != State.Swinging) return;

        // calculate Alpha
        Vector3 ropeHookPoint = new Vector3(ropePositions[0],ropeLength,0f);
        Vector3 diffVector = ropeHookPoint - cube1.position;
        float alpha = Mathf.Atan2(diffVector.x, diffVector.y);

        // Add force to Cube 1
        AddForceToRope(alpha, cube1);
        AddViscousFriction(alpha, cube1);

        // Add force to Cube 2
        AddForceToRope(alpha, cube2);
        AddViscousFriction(alpha, cube2);
    }

    private void AddForceToRope(double alpha, Rigidbody cube)
    {

        float forceG = cube.mass * gravity * (float)Math.Cos(alpha);;
        float forceZ = cube.mass * cube.velocity.magnitude * cube.velocity.magnitude / ropeLength;

        float forceX = (forceG + forceZ) * (float)Math.Sin(alpha);
        float forceY = (forceG + forceZ) * (float)Math.Cos(alpha);

        // finally add force
        cube.AddForce(new Vector3(forceX, forceY, 0f));
    }

    private void AddViscousFriction(double alpha, Rigidbody cube)
    {
        float Cw = 1.1f;
        float rhoAir = 1.2f;

        float velocityX = cube.velocity.x;

        // one side of a cube is 1.5**2
        float cubeSquare = 2.25f; 
        
        Vector3 velocityVec = cube.velocity.normalized;

        cube.AddForce(-0.5f * cubeSquare * rhoAir * Cw * velocityX * velocityX * velocityVec);
    }


}
