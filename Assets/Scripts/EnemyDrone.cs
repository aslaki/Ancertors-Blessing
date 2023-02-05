using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{

    public int MaxHP = 1;

    public int CurrentHP;
    
    public float speed;
    public float attackRange;
    Transform targetDestination;
    public Animator animator;
    Rigidbody2D body;
    public GameObject bulletPrefab;
    public Vector2 bulletSpawnOffset;
    public float bulletVelocity = 2.0f;
    public float weaponCooldownDuration = 0.5f;
    public int damage = 1;
    public float deadAnimationTime;
    private bool isWeaponOnCooldown = false;
    public float shootDelay = 1.5f;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        targetDestination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        CurrentHP=MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;
        if (IsInRange())
        {
            if (!isWeaponOnCooldown)
                StartCoroutine(Shoot());
            body.velocity = Vector2.zero;
        }
        else
        {
            Move();
        }
        
        
    }

    private IEnumerator Shoot()
    {
        isWeaponOnCooldown = true;
        animator.SetTrigger("Attack");

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Enemy_Drone_Attack", gameObject);

        yield return new WaitForSeconds(shootDelay);
        var directionVector = (targetDestination.position - transform.position).normalized;
        var bullet = Instantiate(bulletPrefab, 
            transform.position.ToVector2() + 
            (bulletSpawnOffset * directionVector), Quaternion.identity);
        var bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.velocity = directionVector * bulletVelocity;
        
        yield return new WaitForSeconds(weaponCooldownDuration);
        isWeaponOnCooldown = false;

    }
    
    private void Move()
    {
        var directionVector = (targetDestination.position - transform.position).normalized;
        var facingDirection = Utils.GetHorizontalDirection(directionVector);
        var scale = transform.localScale;
        if (facingDirection == Utils.Direction.Left)
            transform.localScale = new Vector3(-1 * MathF.Abs(scale.x), 
                scale.y, scale.z);
        else
            transform.localScale = new Vector3(MathF.Abs(scale.x), 
                scale.y, scale.z);
        
        body.velocity = directionVector * speed;
    }
    
    private bool IsInRange()
    {
        var distance = Vector3.Distance(transform.position, targetDestination.position);
        return distance <= attackRange;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Enemy_Drone_Hit", gameObject);
            isDead = true;
            body.velocity = Vector2.zero;
            body.constraints = RigidbodyConstraints2D.FreezeAll;
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
