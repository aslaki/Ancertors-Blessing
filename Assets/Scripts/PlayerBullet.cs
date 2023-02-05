using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float lifeTimeSec = 5f;
    public int damage = 1;
    private Timer _lifeTimer = new Timer();
    void Start(){
        _lifeTimer.Start(lifeTimeSec);
    }

    void Update()
    {
        _lifeTimer.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(_lifeTimer.HasElapsed)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collide");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY GOT HIT!");
            other.gameObject.GetComponent<MeleeEnemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
