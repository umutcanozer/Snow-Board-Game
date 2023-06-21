using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float torqueAmount = 10f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 20f;

    [SerializeField] float boostDuration = 4f;
    [SerializeField] float boostCooldown = 10f;
    bool hasCooldown;
    bool canMove = true;

    SurfaceEffector2D _surfaceEffector2D;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>(); //Acces the component which is on a different game object by using FindObjectOfType instead of GetComponent   
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }
   
    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(torqueAmount);
        }else if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-torqueAmount);
        }
        //float turn = Input.GetAxis("Horizontal");
        //rb2d.AddTorque(-turn * torqueAmount);
    }

    void RespondToBoost()
    {
        //if we push up, speed up
        //otherwise stay at normal speed up

        if (Input.GetKeyDown(KeyCode.Space) && !hasCooldown)
        {
            _surfaceEffector2D.speed = boostSpeed;
            StartCoroutine(ActivateCooldown());   //first it calls "ActivateCooldown" and wait for "boostDuration" seconds and then calls next function "ResetMovementVector".
                                                  //After boostDuration timer ends, the function continues running the next commands.
            StartCoroutine(ResetMovementVector());//The same thing happens for that but this time there is no next function, the statement ends.
            
        }

    }
    IEnumerator ResetMovementVector()
    {
        //wait some seconds
        yield return new WaitForSeconds(boostDuration);
        //return to default speed
        _surfaceEffector2D.speed = baseSpeed;
        Debug.Log("Boost ended!");
    }

    IEnumerator ActivateCooldown()
    {
        //disable the ability to use speed boost
        hasCooldown = true;
        yield return new WaitForSeconds(boostCooldown); // the cooldown duration is boostCooldown-boostDuration in game.
        //make the ability to use speed boost enable
        hasCooldown = false;
        Debug.Log("boost ready!");
    }
}
