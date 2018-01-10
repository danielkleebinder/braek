//Particle behaviour
//doesnt work properly btw (not important)

using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour {
    public float destructionTime;

    // Use this for initialization
    void Start() {
        Destroy(gameObject, destructionTime);
    }
}
