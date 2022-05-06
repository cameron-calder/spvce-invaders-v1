using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField]private GameObject enemy;
    private bool isHit;

    private void Start()
    {

        isHit = false;
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }

    private float start = 0.1f;
    private float end = 2;

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

        isHit = true;
        Destroy(gameObject);

    }
}
