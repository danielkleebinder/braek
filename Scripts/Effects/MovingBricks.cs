using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBricks : MonoBehaviour
{

    public float speed = 1.0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        transform.position = new Vector3(p.x + (Mathf.Sin(Time.time * speed)) * 0.05f, p.y, p.z);
    }
}
