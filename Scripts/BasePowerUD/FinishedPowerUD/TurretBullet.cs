using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{

    [SerializeField]
    private float speed = 50.0f;

    private int direction = 1;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.gunShotSFXA.Play();
        GetComponent<Rigidbody>().velocity = new Vector3(0.0f, speed * (float)direction, 0.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
        GetComponent<Rigidbody>().velocity = new Vector3(0.0f, speed * (float)direction, 0.0f);
    }

    public int GetDirection()
    {
        return direction;
    }
}