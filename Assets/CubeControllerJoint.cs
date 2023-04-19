using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControllerJoint : MonoBehaviour
{
    private Rigidbody rigidBody;
    private FixedJoint joint;

    public Rigidbody cubeOne;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != cubeOne) return;

        //Set the anchor point where the wand and blade collide
        ContactPoint contact = collision.contacts[0];
        joint.anchor = transform.InverseTransformPoint(contact.point);
        joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponent<Rigidbody>();
        Debug.Log("After Joint" + rigidBody.velocity.x);
        // Stops objects from continuing to collide and creating more joints
        joint.enableCollision = false;
    }
}
