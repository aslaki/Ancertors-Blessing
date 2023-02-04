using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    Transform targetDestination;
    private PlayerController playerController;

    Rigidbody2D rgdbd2d;

    private void Awake(){
        targetDestination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rgdbd2d = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.GetComponent<PlayerController>()){
            playerController = collision.gameObject.GetComponent<PlayerController>();
            if(!playerController.isImmune)
            {
                playerController.currentHP-=1;
                playerController.GoImmune();
                Debug.Log(playerController.isImmune);
                Debug.Log(playerController.currentHP);
            }
        }
    }

    private void Attack(){
        Debug.Log("Attacking the player.");
    }
}
