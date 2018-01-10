using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasicPower : MonoBehaviour
{
    [SerializeField]
    private float dropSpeed = 4.0f;

    private int dropDirection = -1;

    private bool p1 = false;
    private bool p2 = false;

    protected string m_name;
    protected string m_description;

    protected void Update()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, dropSpeed * (float)dropDirection * (float)GameManager.instance.powerDropDirection, 0.0f));
    }

    protected void OnTriggerEnter(Collider collider)
    {
        p1 = collider.gameObject.CompareTag("P1_Paddle");
        p2 = collider.gameObject.CompareTag("P2_Paddle");

        if (p1 || p2)
        {
            if (p1)
            {
                GameObject.Find("RightPowerText").GetComponent<Text>().text = m_name;
                GameObject.Find("RightPowerDescription").GetComponent<Text>().text = m_description;
            }
            if (p2)
            {
                GameObject.Find("LeftPowerText").GetComponent<Text>().text = m_name;
                GameObject.Find("LeftPowerDescription").GetComponent<Text>().text = m_description;
            }

            Invoke("ClearText", 2.5f);

            PowerStart();
            OnActivation(collider);
            gameObject.SetActive(false);
        }
    }

    public abstract bool IsTerminated();

    protected abstract void OnActivation(Collider collider);
    protected abstract void OnDeactivation();

    protected abstract void PowerStart();

    private void ClearText()
    {
        GameObject.Find("RightPowerText").GetComponent<Text>().text = "";
        GameObject.Find("RightPowerDescription").GetComponent<Text>().text = "";
        GameObject.Find("LeftPowerText").GetComponent<Text>().text = "";
        GameObject.Find("LeftPowerDescription").GetComponent<Text>().text = "";
    }

    public bool IsPlayerOne()
    {
        return p1;
    }

    public bool IsPlayerTwo()
    {
        return p2;
    }

    public int GetPlayerIndex()
    {
        if (p1)
        {
            return 0;
        }
        return 1;
    }

    public void SetDropDirection(int dropDirection)
    {
        this.dropDirection = dropDirection;
    }

    public int GetDropDirection()
    {
        return dropDirection;
    }

    public void SetName(string m_name)
    {
        this.m_name = m_name;
    }

    public string GetName()
    {
        return m_name;
    }

    public void SetDescription(string m_description)
    {
        this.m_description = m_description;
    }

    public string GetDescription()
    {
        return m_description;
    }
}