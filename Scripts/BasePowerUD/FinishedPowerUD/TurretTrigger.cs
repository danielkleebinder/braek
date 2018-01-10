using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TurretTrigger : SelfTerminatingPower
{
    [SerializeField]
    private GameObject turretPrefab;

    private GameObject turret;

    void Start()
    {
        m_name = "Turret";
        m_description = "The turret is able to shoot a couple of times and destroy bricks";
    }

    protected override void OnActivation(Collider collider)
    {
        GameManager.instance.gunPickupSFXA.Play();

        // Cancel if turret already active
        if (GameManager.instance.turretActive[GetPlayerIndex()])
        {
            return;
        }
        GameManager.instance.turretActive[GetPlayerIndex()] = true;

        // Do Player Specific Configurations
        if (IsPlayerOne())
        {
            SpawnTurret(collider, GameManager.instance.powerDropDirection * -1);
        }

        if (IsPlayerTwo())
        {
            SpawnTurret(collider, GameManager.instance.powerDropDirection);
        }
    }

    private void SpawnTurret(Collider collider, int dir)
    {
        // Instantiate Turret
        turret = Instantiate(turretPrefab, collider.gameObject.transform.position, Quaternion.identity) as GameObject;
        turret.transform.parent = collider.gameObject.transform;

        Vector3 pos = collider.gameObject.transform.position;

        // Set directions
        if (dir < 0)
        {
            turret.GetComponent<Turret>().SetDirection(1);
            turret.transform.position = new Vector3(pos.x, pos.y + 1.1f, pos.z);
        }
        else
        {
            turret.GetComponent<Turret>().SetDirection(-1);
            turret.transform.position = new Vector3(pos.x, pos.y - 1.1f, pos.z);
        }
    }

    public override bool IsTerminated()
    {
        if (turret == null)
        {
            return false;
        }
        return turret.GetComponent<Turret>().IsTerminated();
    }
}
