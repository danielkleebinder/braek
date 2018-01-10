//Responsible for ball behaviour

using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    private float ballVelocity = 31.5f;
    private Rigidbody rb;
    private bool ballInPlay;
    private bool arrived; //has the ball arrived on the other side?
    public int numCollisions;



    void Awake()
    {

        arrived = false;
        rb = GetComponent<Rigidbody>();
        numCollisions = 0;
    }

    void FixedUpdate()
    {
        // Clamp ball bounce angle
        float minY = 25.0f;
        if (Mathf.Abs(rb.velocity.y) < minY)
        {
            rb.velocity = new Vector3(rb.velocity.x, (rb.velocity.y < 0) ? -minY : minY, rb.velocity.z);
        }

        float minX = 5.0f;
        if (Mathf.Abs(rb.velocity.x) < minX)
        {
            rb.velocity = new Vector3((rb.velocity.y < 0) ? -minX : minX, rb.velocity.y, rb.velocity.z);
        }

        //"start" ball on leftclick
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballVelocity, ballVelocity, 0));
        }
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        rb.velocity = rb.velocity.normalized * ballVelocity;
    }

    private void OnBecameInvisible()
    {
        /*https://docs.unity3d.com/ScriptReference/Renderer.OnBecameInvisible.html
        //OnBecameInvisible is called when the object is no longer visible by any camera.
        To be safe:*/

        //Useful for testing, hold Ctrl+D to duplicate balls and see what happens
        Debug.Log("!!!!!!!!!!!!!!!!!!!!\nBALL HAD TO BE DESTROYED, THIS SHOULDN'T HAVE HAPPENED\n!!!!!!!!!!!!!!!!!!!!\n");

        //Instantiate a new Ball, doesnt work, no mesh renderer?!?!
        //Instantiate(this.gameObject, new Vector3(2,2,0), this.transform.rotation);

        Destroy(gameObject);
    }


    public void setArrived(bool hasBallArrived)
    {
        //used by Switching colliders "SwitchBoxCollider.cs", "SwitchCapsuleCollider.cs"
        arrived = hasBallArrived;
    }

    public bool getArrived()
    {
        //used by Switching colliders "SwitchBoxCollider.cs", "SwitchCapsuleCollider.cs"
        return arrived;
    }

    //Collision counter
    public void OnCollisionEnter(Collision col)
    {
        numCollisions++;
    }

    public int getNumOfCollisions()
    {
        return numCollisions;
    }
}