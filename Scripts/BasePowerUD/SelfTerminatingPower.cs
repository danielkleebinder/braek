using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelfTerminatingPower : BasicPower
{
    protected override void OnDeactivation() { }
    protected override void PowerStart() { }
}