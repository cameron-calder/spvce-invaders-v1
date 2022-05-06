using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** Needs Data Script to be created.**//
public class ProjectileController : MonoBehaviour
{

    private bool isHit;

    private void Start()
    {

        isHit = false;

    }

    private float start = 0.1f;
    private float end = 2;

    // Update is called once per frame
    private void Update()
    {

        //This will destroy the projectile over a length of time if it doesn't collide with an object.
        if(!isHit){
            if(start < end){

                start+= Time.deltaTime;

            }else{

                Destroy(gameObject);

            }

        }

    }

    private void OnCollisionEnter2D(Collision2D other){

        //Tells projectile it has collided with an object to start timer.
        isHit = true;
        
        //Destroys itself upon contact.
        Destroy(gameObject);

    }
}
