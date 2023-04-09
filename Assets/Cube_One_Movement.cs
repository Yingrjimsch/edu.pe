using System;
using UnityEngine;

public class Cube_One_Movement : MonoBehaviour
{
    private Rigidbody rigidBody;

    public int pushForce; // N/m
    public float springConstant = 2;

    private float startCompressionX;
    private float auslenkung;

    private bool accelerate = true;


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
        if (Math.Abs(rigidBody.velocity.x) > 2 && accelerate) 
        {
            startCompressionX = rigidBody.position.x;
            accelerate = false;
        }
        if (!accelerate) {
            auslenkung = Math.Abs(auslenkung - startCompressionX);
            Debug.Log("2 erreicht");
            rigidBody.AddForce(new Vector3(1 - springConstant * auslenkung, 0f, 0f));    
            return;

        }
        float forceX; // N
        forceX = 1;

        Debug.Log(forceX);
        rigidBody.AddForce(new Vector3(forceX, 0f, 0f));
        Debug.Log(Math.Abs(rigidBody.velocity.x));
        /*if (Math.Abs(rigidBody.velocity.x) > 2 && accelerate)
        {
            //accelerate = false;
            rigidBody.AddForce(new Vector3(rigidBody.velocity.x * -1, 0f, 0f));
        //}

        /*if (accelerate)
        {
            forceX = rigidBody.position.x * pushForce;
            rigidBody.AddForce(new Vector3(forceX, 0f, 0f));
        }*/
    }

    void OnApplicationQuit()
    {
    }
}
