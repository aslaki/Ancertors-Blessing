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
    public float deadAnimationTime;
    Rigidbody2D rgdbd2d;

    public int MaxHP = 1;
    public int CurrentHP;
    private bool isDead = false;
    private void Awake(){
        targetDestination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rgdbd2d = GetComponent<Rigidbody2D>();
        CurrentHP = MaxHP;
    }


    void FixedUpdate()
    {
        if (isDead)
            return;
        Vector3 directionVector = (targetDestination.position - transform.position).normalized;
        var facingDirection = Utils.GetHorizontalDirection(directionVector);
        var scale = transform.localScale;
        if (facingDirection == Utils.Direction.Left)
            transform.localScale = new Vector3( MathF.Abs(scale.x), 
                scale.y, scale.z);
        else
            transform.localScale = new Vector3(-1 * MathF.Abs(scale.x), 
                scale.y, scale.z);
        
        rgdbd2d.velocity = directionVector * speed;
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.GetComponent<PlayerController>()){
            playerController = collision.gameObject.GetComponent<PlayerController>();
            animator.SetBool("IsAttacking", true);
            if(!playerController.isImmune)
            {
                playerController.TakeDamage();
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
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Enemy_Human_Hit", gameObject);
            isDead = true;
            rgdbd2d.velocity = Vector2.zero;
            rgdbd2d.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("IsDying", true);
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        // TODO: Should probably use animator here to check if finished
        yield return new WaitForSeconds(deadAnimationTime);
        Destroy(gameObject);
    }
}
