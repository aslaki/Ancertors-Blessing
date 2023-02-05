using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    public Image[] currentHeartsUI;

    private bool isUpPressed;
    private bool isDownPressed;
    private bool isLeftPressed;
    private bool isRightPressed;
    private bool isShootBtnPressed;
    private Vector2 shootDirection;
    private bool isWeaponOnCooldown;

    public Rigidbody2D body;
    public Vector2 velocity;

    public Camera playerCamera;
    public GameObject bulletPrefab;

    public Boolean isImmune;
    public int immuneForSeconds;
    public int maxHP;
    public int currentHP;


    public float weaponCooldownDuration = 0.5f;
    public float bulletSpawnOffset = 0.5f;
    public float bulletVelocity = 2.0f;
    
    public string animation_walk = "PlayerWalk";
    public string animation_idle = "PlayerIdle";

    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(body);
        }

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            Assert.IsNotNull(playerCamera);
        }

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        isImmune = false;
        currentHP = maxHP;
        RerenderHPUI();
    }

    // Update is called once per frame
    void Update()
    {
        isUpPressed = Input.GetKey(KeyCode.W);
        isDownPressed = Input.GetKey(KeyCode.S);
        isLeftPressed = Input.GetKey(KeyCode.A);
        isRightPressed = Input.GetKey(KeyCode.D);
        isShootBtnPressed = Input.GetKey(KeyCode.Mouse0);
        
        var mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        var position = transform.position.ToVector2();
        shootDirection = new Vector2(mousePosition.x - position.x, mousePosition.y - position.y).normalized;
        Debug.DrawLine(position, position + (shootDirection * 2.0f), Color.red);
    }


    private void FixedUpdate()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        var newVelocity = new Vector2(0, 0);
        var isMoving = false;
        if (isDownPressed)
        {
            newVelocity.y = -velocity.y;
            isMoving = true;
        }else if (isUpPressed)
        {
            newVelocity.y = velocity.y;
            isMoving = true;
        }

        if (isRightPressed)
        {
            newVelocity.x = velocity.x;
            isMoving = true;
        }else if (isLeftPressed)
        {
            newVelocity.x = -velocity.x;
            isMoving = true;
        }
        body.velocity = newVelocity;
        // Flip sprite based on direction
        var facingDirection = Utils.GetHorizontalDirection(shootDirection);
        var scale = transform.localScale;
        if (facingDirection == Utils.Direction.Left)
            transform.localScale = new Vector3(-1 * MathF.Abs(scale.x), 
                scale.y, scale.z);
        else
            transform.localScale = new Vector3(MathF.Abs(scale.x), 
                scale.y, scale.z);
        if(isMoving)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }

    private void Shoot()
    {
        if(!isShootBtnPressed || isWeaponOnCooldown) return;
        
        var bullet = GameObject.Instantiate(bulletPrefab, 
            transform.position + (Vector3)shootDirection * bulletSpawnOffset, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletVelocity;
        StartCoroutine(StartCooldown());
    }
    
    // Process weapon cooldown
    private IEnumerator StartCooldown()
    {
        isWeaponOnCooldown = true;
        yield return new WaitForSeconds(weaponCooldownDuration);
        isWeaponOnCooldown = false;
    }

    private void RerenderHPUI(){
        for(int i=0; i<currentHeartsUI.Length; i++)
        {
            if(i+1 <= currentHP){
                currentHeartsUI[i].enabled = true;
            } else {
                currentHeartsUI[i].enabled = false;
            }
        }
    }

    public void IncreaseFireRate() {
        weaponCooldownDuration = weaponCooldownDuration*0.5f;
    }

    public void Heal(){
        if (currentHP <= 6)
        {
            currentHP+=1;
            RerenderHPUI();
        }
    }

    public void TakeDamage(){
        currentHP -=1;
        GoImmune();
        RerenderHPUI();
        if(currentHP >= 0)
        {
            gameManager.GameOver(false);
        }
    }

    public void GoImmune()
    {
        StartCoroutine(ImmunityControl());
    }

    private IEnumerator ImmunityControl(){
        isImmune = true;
        yield return new WaitForSeconds(immuneForSeconds);
        isImmune = false;
    }
}
