using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePaddleSize : TimeTerminatedPower
{
    protected float increase = 1.0f;

    protected GameObject paddle;

    void Start()
    {
        m_name = "Increase Paddle Size";
        m_description = "Increases the paddle size of the player";

        lastingFor = 25.0f;
    }

    protected override void OnActivation(Collider collider)
    {
        GameManager.instance.sizeBiggerSFXA.Play();

        paddle = collider.gameObject;
        Vector3 ls = paddle.transform.localScale;

        float ns = Mathf.Clamp(ls.y + increase, 2, 8);

        paddle.transform.localScale = new Vector3(ls.x, ns, ls.z);
    }

    protected override void OnDeactivation()
    {
        Vector3 ls = paddle.transform.localScale;

        float ns = Mathf.Clamp(ls.y - increase, 2, 8);

        paddle.transform.localScale = new Vector3(ls.x, ns, ls.z);
    }

    public void SetSizeIncrease(float increase)
    {
        this.increase = increase;
    }

    public float GetSizeIncrease()
    {
        return increase;
    }
}
