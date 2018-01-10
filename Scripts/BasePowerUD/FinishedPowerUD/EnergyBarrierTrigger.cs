using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarrierTrigger : SelfTerminatingPower
{

    [SerializeField]
    private GameObject energyBarrierPrefab;

    private GameObject energyBarrier;

    void Start()
    {
        m_name = "Energy Barrier";
        m_description = "The energy barrier is able to block off one ball from going to the other players side";
    }


    protected override void OnActivation(Collider collider)
    {
        GameManager.instance.energybarrierSFXA.Play();
        // Cancel if barrier already active
        if (GameManager.instance.barrierActive[GetPlayerIndex()])
        {
            return;
        }
        GameManager.instance.barrierActive[GetPlayerIndex()] = true;

        // Instantiate Barrier
        Vector3 pos = collider.gameObject.transform.position;
        if (IsPlayerOne())
        {
            energyBarrier = Instantiate(
                energyBarrierPrefab,
                new Vector3(0, pos.y - 2.5f * (float)GameManager.instance.powerDropDirection, 0),
                Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
        }

        if (IsPlayerTwo())
        {
            energyBarrier = Instantiate(
                energyBarrierPrefab,
                new Vector3(0, pos.y + 2.5f * (float)GameManager.instance.powerDropDirection, 0),
                Quaternion.Euler(0.0f, 0.0f, -90.0f)) as GameObject;
        }
    }

    public override bool IsTerminated()
    {
        if (energyBarrier == null)
        {
            return false;
        }
        return energyBarrier.GetComponent<EnergyBarrier>().IsTerminated();
    }
}
