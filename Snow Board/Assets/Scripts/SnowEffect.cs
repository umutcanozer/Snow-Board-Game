using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem snowEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.collider.tag == "Surface") snowEffect.Play();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Surface") snowEffect.Stop();      
    }
}
