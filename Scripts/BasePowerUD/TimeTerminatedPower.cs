using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeTerminatedPower : BasicPower
{

    protected float lastingFor = 10.0f;

    private float counter = 0.0f;

    protected override void PowerStart()
    {
        counter = 0.0f;
        Invoke("TimeoutReceiver", lastingFor);
    }

    private void TimeoutReceiver()
    {
        OnDeactivation();
        Destroy(gameObject);
    }

    protected new void OnTriggerEnter(Collider collider)
    {
        counter = 0;
        base.OnTriggerEnter(collider);
    }

    public float TimeRemaining()
    {
        return lastingFor - counter;
    }

    public override bool IsTerminated()
    {
        return TimeRemaining() <= 0.0f;
    }

    public void SetLastingFor(float lastingFor)
    {
        this.lastingFor = lastingFor;
    }

    public float GetLastingFor()
    {
        return lastingFor;
    }
}