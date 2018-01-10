using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : TimeTerminatedPower
{

    private Vector3 gravity;
    private string ballTag = "notnull";
    [SerializeField]
    private char axis;
    [SerializeField]
    private float gravityConstant;

    void Start()
    {
        m_name = "Gravity Pull";
        m_description = "Pulls the balls to the bottom";

        lastingFor = 8.0f;
    }

    private void Awake()
    {
        //Get gravity property for the world
        gravity = Physics.gravity;

    }

    private void FixedUpdate()
    {
        setFakeGravityAxisAndConstant();
    }


    protected override void OnActivation(Collider collider)
    {
        GameManager.instance.gravityPullSFXA.Play();

        //Debug.Log("Power UD collided with: " + col.gameObject.tag);
        if (IsPlayerOne())
        {
            ballTag = "P1_Ball";
            activatePUD();
        }
        if (IsPlayerTwo())
        {
            ballTag = "P2_Ball";
            activatePUD();
        }
    }

    protected override void OnDeactivation()
    {
        GameObject.FindGameObjectWithTag(ballTag).GetComponent<Rigidbody>().useGravity = false;
    }

    private void activatePUD()
    {
        //get ball and set gravity on
        GameObject.FindGameObjectWithTag(ballTag).GetComponent<Rigidbody>().useGravity = true;
    }


    public void setFakeGravityAxisAndConstant()
    {
        switch (axis)
        {
            case 'x':
                gravity.x = gravityConstant;
                break;
            case 'y':
                gravity.y = gravityConstant;
                break;
            case 'z':
                Debug.Log("2D, bro");
                break;
            default:
                Debug.Log("Not an axis!");
                break;
        }
        Physics.gravity = gravity;
    }
}
