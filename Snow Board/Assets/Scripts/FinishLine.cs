using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishEffect;
    AudioSource finishSound;
    float delayTime = 1.5f;
    void Start()
    {
        finishSound= GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !FindObjectOfType<Crash>().hasCrashed)
        {
            FindObjectOfType<Crash>().hasCrashed = true;
            Debug.Log("Finished!");
            finishEffect.Play();    
            finishSound.Play();
            Invoke("ReloadScene", delayTime);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
