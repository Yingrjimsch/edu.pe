using UnityEngine;

public class Cube : MonoBehaviour
{
	private Rigidbody rigidBody;
	private State state;
	private float forceX;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		state = State.Accelerate;
	}

	private void FixedUpdate()
	{
		Debug.Log(state.ToString() + " with velocity: " + GetVelocity() + " and force:" + forceX);
	}

	public State GetState()
	{
		return state;
	}

	public void SetState(State state)
	{
		this.state = state;
	}

	public void SetForceX(float forceX = 0f)
	{
		this.forceX = forceX;
		rigidBody.AddForce(new Vector3(forceX, 0f, 0f));
	}

	public float GetVelocity()
	{
		return rigidBody.velocity.x;
	}

	public float GetPositionX()
	{
		return rigidBody.position.x;
	}
}
