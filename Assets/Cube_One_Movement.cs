using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.IO;

public class Cube_One_Movement : MonoBehaviour
{
    private Rigidbody rigidBody;

    public int pushForce; // N/m
    public float springConstant = 0.0002f;
    public float maxSpeed = 2;
    private float startCompressionX;
    private float auslenkung;
    private float currentTimeStep; // s
    public float forceX;
    private List<List<float>> timeSeries;

    private bool accelerate = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate()
    {
        if (Math.Abs(rigidBody.velocity.x) >= 2 && accelerate) 
        {
            Debug.Log("start");
            startCompressionX = rigidBody.position.x;
            accelerate = false;
        }
        if (!accelerate) {
            auslenkung = rigidBody.position.x <= startCompressionX ? 0 : Math.Abs(rigidBody.position.x - startCompressionX);
            Debug.Log("Auslenkung " + auslenkung);
            float springForce = -springConstant * auslenkung;
            Debug.Log("2 erreicht");
            forceX = forceX == 0 ? springForce : forceX + springForce;
            rigidBody.AddForce(new Vector3(forceX, 0f, 0f));

            currentTimeStep += Time.deltaTime;
            timeSeries.Add(new List<float>() { currentTimeStep, rigidBody.position.x , GameObject.Find("Cube_Two").GetComponent<Rigidbody>().position.x,
            rigidBody.velocity.x, GameObject.Find("Cube_Two").GetComponent<Rigidbody>().velocity.x,
            rigidBody.velocity.x * rigidBody.mass, GameObject.Find("Cube_Two").GetComponent<Rigidbody>().velocity.x * GameObject.Find("Cube_Two").GetComponent<Rigidbody>().mass });
            return;
        }
        forceX = 1;
        Debug.Log(forceX);
        rigidBody.AddForce(new Vector3(forceX, 0f, 0f));
        Debug.Log(Math.Abs(rigidBody.velocity.x));

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() { currentTimeStep, rigidBody.position.x , GameObject.Find("Cube_Two").GetComponent<Rigidbody>().position.x,
        rigidBody.velocity.x, GameObject.Find("Cube_Two").GetComponent<Rigidbody>().velocity.x,
        rigidBody.velocity.x * rigidBody.mass, GameObject.Find("Cube_Two").GetComponent<Rigidbody>().velocity.x * GameObject.Find("Cube_Two").GetComponent<Rigidbody>().mass });
    }

    void OnApplicationQuit()
    {
        WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV()
    {
        using (var streamWriter = new StreamWriter("time_series_exercise2.csv"))
        {
            //streamWriter.WriteLine("t,x(t),v(t),F(t) (added)");
            streamWriter.WriteLine("t,x_cubeOne(t),x_cubeTwo(t), v_cubeOne(t),v_cubeTwo(t), p_cubeOne(t), p_cubeTwo(t)");
            Debug.Log(timeSeries);
            foreach (List<float> timeStep in timeSeries)
            {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}
