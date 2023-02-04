using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
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

    public float weaponCooldownDuration = 0.5f;
    public float bulletSpawnOffset = 0.5f;
    public float bulletVeloity = 2.0f;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        isUpPressed = Input.GetKey(KeyCode.UpArrow);
        isDownPressed = Input.GetKey(KeyCode.DownArrow);
        isLeftPressed = Input.GetKey(KeyCode.LeftArrow);
        isRightPressed = Input.GetKey(KeyCode.RightArrow);
        isShootBtnPressed = Input.GetKey(KeyCode.Space);
        
        var mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        shootDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        Debug.DrawLine(shootDirection, transform.position, Color.red);
    }


    private void FixedUpdate()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        var newVelocity = new Vector2(0, 0);
        if (isDownPressed)
        {
            newVelocity.y = -velocity.y;
        }else if (isUpPressed)
        {
            newVelocity.y = velocity.y;
        }

        if (isRightPressed)
        {
            newVelocity.x = velocity.x;
        }else if (isLeftPressed)
        {
            newVelocity.x = -velocity.x;
        }
        body.velocity = newVelocity;
    }

    private void Shoot()
    {
        if(!isShootBtnPressed || isWeaponOnCooldown) return;
        
        var bullet = GameObject.Instantiate(bulletPrefab, 
            transform.position + (Vector3)shootDirection * bulletSpawnOffset, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletVeloity;
        StartCoroutine(StartCooldown());
    }
    
    // Process weapon cooldown
    private IEnumerator StartCooldown()
    {
        isWeaponOnCooldown = true;
        yield return new WaitForSeconds(weaponCooldownDuration);
        isWeaponOnCooldown = false;
    }


}
