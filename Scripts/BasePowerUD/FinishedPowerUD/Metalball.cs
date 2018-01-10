using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Metalball : TimeTerminatedPower
{
    [SerializeField]
    private Material stdMat;
    [SerializeField]
    private Material metalMat;
    [SerializeField]
    private PhysicMaterial stdPhyMat;
    [SerializeField]
    private PhysicMaterial metalPhyMat;

    private GameObject affectedBall = null;
    private GameObject activePrefab;

    void Start()
    {
        m_name = "Metal Ball";
        m_description = "The metal ball if way heavier and is able to throw the other ball out of its trajectory without getting affected itself";

        lastingFor = 8.0f;
    }


    protected override void OnActivation(Collider collider)
    {
        GameManager.instance.metallballSFXA.Play();

        if (IsPlayerOne())
        {
            affectedBall = GameObject.FindGameObjectWithTag("P1_Ball");
        }

        if (IsPlayerTwo())
        {
            affectedBall = GameObject.FindGameObjectWithTag("P2_Ball");
        }

        if (IsPlayerOne() || IsPlayerTwo())
        {
            affectedBall.GetComponent<Rigidbody>().mass = 50;
            affectedBall.GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            affectedBall.GetComponent<MeshRenderer>().material = metalMat;
            affectedBall.GetComponent<SphereCollider>().material = metalPhyMat;
        }
    }

    protected override void OnDeactivation()
    {
        affectedBall.GetComponent<Rigidbody>().mass = 1;
        affectedBall.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        affectedBall.GetComponent<MeshRenderer>().material = stdMat;
        affectedBall.GetComponent<SphereCollider>().material = stdPhyMat;
    }
}