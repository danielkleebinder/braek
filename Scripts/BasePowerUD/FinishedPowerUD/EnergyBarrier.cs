using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarrier : MonoBehaviour
{
    public int hits = 3;

    void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(AnimateScale(true));
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "P1_Ball" || other.tag == "P2_Ball")
        {
            hits--;
            if (hits <= 0)
            {
                StartCoroutine(AnimateScale(false));
                GameManager.instance.barrierActive[transform.position.y < 0 ? 0 : 1] = false;
            }
        }
    }

    private IEnumerator AnimateScale(bool up)
    {
        Vector3 scale = transform.localScale;
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * 3.5f;
            if (up)
            {
                yield return transform.localScale = Vector3.Lerp(new Vector3(1.0f, 0.0f, 0.1f), new Vector3(1.0f, 1.0f, 0.1f), Mathf.SmoothStep(0.0f, 1.0f, t));
            }
            else
            {
                yield return transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1.0f, 1.0f, 0.1f), Mathf.SmoothStep(0.0f, 1.0f, t));
            }
        }

        t = 0;
        while (t < 1.0f)
        {
            t += Time.deltaTime * 3.5f;
            if (up)
            {
                yield return transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 0.1f), Vector3.one, Mathf.SmoothStep(0.0f, 1.0f, t));
            }
            else
            {
                yield return transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 0.1f), new Vector3(1.0f, 0.0f, 0.1f), Mathf.SmoothStep(0.0f, 1.0f, t));
            }
        }

        if (!up)
        {
            Destroy(gameObject);
        }
    }

    public bool IsTerminated()
    {
        return hits <= 0;
    }
}
