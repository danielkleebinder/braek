using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTrigger : TimeTerminatedPower
{
    [SerializeField]
    private GameObject fogPrefab;

    private GameObject activeFog;

    void Start()
    {
        m_name = "Fog";
        m_description = "Put fog above your paddle";

        lastingFor = 8.0f;
    }

    protected override void OnActivation(Collider collider)
    {
        GameManager.instance.fogSpawnSFXA.Play();
        GameObject currentPaddle = collider.gameObject;
        Transform pt = currentPaddle.transform;

        activeFog = Instantiate(fogPrefab, pt.position, fogPrefab.transform.rotation);
        activeFog.GetComponentInChildren<Fog>().currentPaddle = currentPaddle;

        if (IsPlayerTwo())
        {
            activeFog.transform.Rotate(0.0f, 0.0f, 180.0f);
        }
    }

    protected override void OnDeactivation()
    {
        activeFog.GetComponentInChildren<ParticleSystem>().Stop();
    }
}