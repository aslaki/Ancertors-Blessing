using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float lifeTimeSec = 5f;
    public int damage = 1;
    private Timer _lifeTimer = new Timer();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<MeleeEnemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
