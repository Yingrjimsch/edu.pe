using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MovingCubeLogger : MonoBehaviour
{
	public Rigidbody cubeOne;
	public Rigidbody cubeTwo;

	private float currentTimeStep; // s
	private List<List<float>> timeSeries;
	// Start is called before the first frame update
	void Start()
	{
		timeSeries = new List<List<float>>();

	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		currentTimeStep += Time.deltaTime;
		timeSeries.Add(new List<float>() { currentTimeStep, cubeOne.position.x , cubeTwo.position.x,
		cubeOne.velocity.x, cubeTwo.velocity.x,
		cubeOne.velocity.x * cubeOne.mass,
		cubeTwo.velocity.x * cubeTwo.mass });
	}

	void OnApplicationQuit()
	{
		WriteTimeSeriesToCSV();
	}

	void WriteTimeSeriesToCSV()
	{
		using var streamWriter = new StreamWriter("time_series_exercise2.csv");
		streamWriter.WriteLine("t,x_cubeOne(t),x_cubeTwo(t),v_cubeOne(t),v_cubeTwo(t),p_cubeOne(t),p_cubeTwo(t)");
		Debug.Log(timeSeries);
		foreach (List<float> timeStep in timeSeries)
		{
			streamWriter.WriteLine(string.Join(",", timeStep));
			streamWriter.Flush();
		}
	}
}
