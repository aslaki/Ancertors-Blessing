using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    Transform targetDestination;
    private PlayerController playerController;

    public Animator animator;
    Rigidbody2D rgdbd2d;

    public int MaxHP = 1;
    public int CurrentHP;
    
    private void Awake(){
        targetDestination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rgdbd2d = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector3 directionVector = (targetDestination.position - transform.position).normalized;
        var facingDirection = Utils.GetHorizontalDirection(directionVector);
        var scale = transform.localScale;
        if (facingDirection == Utils.Direction.Left)
            transform.localScale = new Vector3(-1 * MathF.Abs(scale.x), 
                scale.y, scale.z);
        else
            transform.localScale = new Vector3(MathF.Abs(scale.x), 
                scale.y, scale.z);
        
        rgdbd2d.velocity = directionVector * speed;
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.GetComponent<PlayerController>()){
            playerController = collision.gameObject.GetComponent<PlayerController>();
            animator.SetBool("IsAttacking", true);
            if(!playerController.isImmune)
            {
                playerController.currentHP-=1;
                playerController.GoImmune();
                Debug.Log(playerController.isImmune);
                Debug.Log(playerController.currentHP);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>()){
            animator.SetBool("IsAttacking", false);
        }
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
