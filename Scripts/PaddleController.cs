using UnityEngine;
using System;
using System.Collections;

public class PaddleController : MonoBehaviour
{

    [SerializeField]
    private float speed = 60.5f; //paddle speed
    private string inputAxis = null; //Check input manager for axes
                                     //private string threeDimSwitchAxis = null; //used for switching dimensions! EXPERIMENTAL

    private Vector3 playerPos; //paddle new position
    private float paddleBounds = 18.0f;

    [SerializeField]
    private bool customReflectionsActive = false; //Custom reflections

    //SoundFX to choose 1 randomly on collsion.
    System.Random random;
    private int soundSource;
    private AudioSource sound1;
    private AudioSource sound2;


    // Use this for initialization
    void Start()
    {
        customReflectionsActive = true;

        random = new System.Random();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        paddleBounds = 22.0f - transform.localScale.y;

        float posX = transform.position.x + Input.GetAxisRaw(inputAxis) * speed * Time.deltaTime;
        //Debug.Log(inputAxis + "     " + Input.GetAxis(inputAxis));
        playerPos = new Vector3(Mathf.Clamp(posX, -paddleBounds, paddleBounds), transform.position.y, transform.position.z);
        transform.position = playerPos;


        //    EXPERIMENTAL FEATURE: 3D Movement
        if (Input.GetKeyDown(KeyCode.M))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
        if (Input.GetKeyDown(KeyCode.N))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);


    }

    public void setInputAxis(string axis)
    {
        inputAxis = axis;
    }

    public void setSounds(AudioSource collision_sound1, AudioSource collision_sound2)
    {
        sound1 = collision_sound1;
        sound2 = collision_sound2;
    }

    //Custom paddle reflections
    public void setCustomReflections(bool active)
    {
        customReflectionsActive = active;
    }

    private void playSound()
    {
        soundSource = random.Next(0, 2);

        if (soundSource == 1) sound1.Play();
        else sound2.Play();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //plays reflection Sound
        playSound();

        if (customReflectionsActive)
        {
            if (collision.collider.CompareTag("P1_Ball"))
            {
                //Debug.Log("Collision with ball");
                Vector3 firstContactPoint = collision.contacts[0].point;
                //Debug.Log("Global contact point: " + firstContactPoint);
                Vector3 localContactPoint = collision.transform.InverseTransformPoint(transform.position);
                //Debug.Log("Local contact point ~(-3 to +3): " + localContactPoint);
                //Divide by size of paddle
                localContactPoint.x = (localContactPoint.x / transform.localScale.y) * 90;
                //Debug.Log("Local contact point ~(-90 to +90): " + localContactPoint);
                deflect(localContactPoint, collision.gameObject);
            }
        }
    }

    private void deflect(Vector3 contactPoint, GameObject ball)
    {
        //Dont deflect programatically if ball coming from below
        if (contactPoint.y <= 0)
        {
            //Use about 500, 5000 is a visual help for testing
            if (contactPoint.x >= -110 && contactPoint.x <= -60)
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(500, 500, 0));
            if (contactPoint.x > -60 && contactPoint.x <= -30)
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(500, 500, 0));

            if (contactPoint.x > -30 && contactPoint.x <= 0)
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
            if (contactPoint.x > 0 && contactPoint.x <= 30)
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(-0, 500, 0));

            if (contactPoint.x > 30 && contactPoint.x <= 60)
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(-500, 500, 0));
            if (contactPoint.x > 60 && contactPoint.x <= 110)
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(-500, 500, 0));
        }
    }

    public float getClampValue()
    {
        return paddleBounds;
    }
    public void setPaddleBounds(float bounds)
    {
        paddleBounds = bounds;
    }
    public float getPaddleBounds()
    {
        return paddleBounds;
    }
}
