using UnityEngine;
using System.Collections;

public class SwitchBoxCollider : MonoBehaviour
{
    //sets the ball state to "arrived on the other side" (->otherwise infinite switches)
    private Ball[] scriptsOfEnteredBalls;

    void OnTriggerEnter(Collider col) {
        //Get all balls that entered
        scriptsOfEnteredBalls = col.gameObject.GetComponentsInParent<Ball>();
        foreach (Ball ballScript in scriptsOfEnteredBalls)
        {
            if (GetComponentInChildren<BoxCollider>().GetType() == typeof(BoxCollider))
            { //chose the matching child collider
                /*Debug.Log("Trigger inside " + GetComponent<Collider>().GetType() +
                    "\nArrived of " + col.gameObject.name +
                    " set to " + ballScript.getArrived());
                    */
                ballScript.setArrived(true); //set arrived
            }
        }
    }
}
