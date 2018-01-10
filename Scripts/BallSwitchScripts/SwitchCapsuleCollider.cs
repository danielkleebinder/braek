using UnityEngine;
using System.Collections;

public class SwitchCapsuleCollider : MonoBehaviour {

    //responsible for switching the ball to the other side
    //checks with help of ball property "arrived" if the ball is allowed to switch (->otherwise infinite switches)

    private Ball[] scriptsOfEnteredBalls;

    void OnTriggerEnter(Collider col) {
    
        GameManager.instance.switchZoneSFXA.Play();

        //Get all balls that entered
        scriptsOfEnteredBalls = col.gameObject.GetComponentsInParent<Ball>();

        foreach (Ball ballScript in scriptsOfEnteredBalls)
        {
            if (ballScript.getArrived())
            {
                //Debug.Log("Trigger inside " + GetComponent<Collider>().GetType() + "\nArrived: " + ballScript.getArrived());

                Transform ballTrans = col.GetComponent<Transform>();
                ballTrans.position = new Vector3(ballTrans.position.x, ballTrans.position.y * (-1), ballTrans.position.z);
                //Debug.Log("changed pos");
                ballScript.setArrived(false);
            }
        }
    }
}
