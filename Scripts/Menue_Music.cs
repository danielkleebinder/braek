using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Menue_Music : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Fade_in());
    }


    // Update is called once per frame
    void Update()
    {
         
    }

    IEnumerator Fade_in()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = 0;
        audio.Play();

        while (audio.volume < 1.0)
        {
            audio.volume += Time.deltaTime;
            yield return new WaitForSeconds((float)0.5);
        }
        Debug.Log("Audio is now starting!");
    }
}
