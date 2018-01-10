using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour
{
    public GameObject brickParticle;
    public GameObject power;
    public GameObject destructionSound;

    private AudioSource sound;

    void Awake()
    {
        sound = destructionSound.GetComponent(typeof(AudioSource)) as AudioSource;
    }

    void OnCollisionEnter(Collision other)
    {
        AudioSource.PlayClipAtPoint(sound.clip, transform.position);

        Instantiate(brickParticle, transform.position, Quaternion.identity);

        float rnd = Random.Range(-1.0f, 3.0f);
        if (rnd < 0)
        {
            int powerIndex = (int)Random.Range(0.0f, 7.0f);

            switch (powerIndex)
            {
                case 0: power = GameManager.instance.fogPowerDown; break;
                case 1: power = GameManager.instance.gravityPullPowerDown; break;
                case 2: power = GameManager.instance.energyBarrierPowerUp; break;
                case 3: power = GameManager.instance.metalballPowerUp; break;
                case 4: power = GameManager.instance.turretPowerUp; break;
                case 5: power = GameManager.instance.sizeIncreasePowerUp; break;
                case 6: power = GameManager.instance.sizeDecreasePowerDown; break;
            }

            BasicPower bp = Instantiate(power, transform.position, Quaternion.identity).GetComponent<BasicPower>();
            bp.SetDropDirection(transform.position.y <= 0 ? -1 : 1);
        }

        Destroy(gameObject);
    }
}