using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreasePaddleSize : IncreasePaddleSize
{

    // Use this for initialization
    void Start()
    {
        m_name = "Decrease Paddle Size";
        m_description = "Decreases the paddle size of the player";

        lastingFor = 8.0f;
        increase = -1.0f;
    }

    protected override void OnActivation(Collider collider)
    {
      GameManager.instance.sizeSmallerSFXA.Play();

      paddle = collider.gameObject;
      Vector3 ls = paddle.transform.localScale;

      float ns = Mathf.Clamp(ls.y + increase, 2, 8);

      paddle.transform.localScale = new Vector3(ls.x, ns, ls.z);
    }
}
