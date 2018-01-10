using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light lt;

    private float minFlickerSpeed = 0.05f;
    private float maxFlickerSpeed = 0.15f;

    private float startIntensity = 3.0f;

    // Use this for initialization
    void Start()
    {
        lt = GetComponent<Light>();
        startIntensity = lt.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0.0f, 300.0f) <= 1.0f)
        {
            StartCoroutine(Flickering());
        }
    }

    IEnumerator Flickering()
    {
        for (int i = 0; i < (int)Random.Range(3, 5); i++)
        {
            lt.intensity = Random.Range(0.0f, startIntensity);
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        }
        lt.intensity = startIntensity;
    }
}
