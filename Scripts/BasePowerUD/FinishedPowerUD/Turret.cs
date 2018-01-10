using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject particlesPrefab;

    public int shots = 5;

    private int direction = 1;

    // Use this for initialization
    void Start()
    {
        GameObject.Find("RemainingShots").GetComponent<TextMesh>().text = "" + shots;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsTerminated())
        {
            Instantiate(particlesPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.turretActive[direction > 0 ? 0 : 1] = false;
            return;
        }

        KeyCode kc = KeyCode.Space;
        if (direction > 0)
        {
            kc = KeyCode.RightControl;
        }

        if (Input.GetKeyDown(kc))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<TurretBullet>().SetDirection(direction);
            shots--;

            GameObject.Find("RemainingShots").GetComponent<TextMesh>().text = "" + shots;
        }
    }

    public bool IsTerminated()
    {
        return shots <= 0;
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    public int GetDirection()
    {
        return direction;
    }
}