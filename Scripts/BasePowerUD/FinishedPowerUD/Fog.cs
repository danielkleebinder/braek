using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public GameObject currentPaddle;

    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (currentPaddle != null)
        {
            Vector3 pos = currentPaddle.transform.position;
            pos.x = Mathf.Clamp(pos.x, -9.0f, 9.0f);
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
        }
    }
}