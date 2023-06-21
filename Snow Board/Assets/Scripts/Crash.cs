using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash : MonoBehaviour
{
    float delayTime = 0.8f;
    [SerializeField ] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSound;
    AudioSource _audioSource; //AudioClip + AudioSource must be more effective and clean when you're using more than one audio clip.
    public bool hasCrashed;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Surface" && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            Debug.Log("Crashed!");
            crashEffect.Play();
            _audioSource.PlayOneShot(crashSound);
            Invoke("ReloadScene", delayTime);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
