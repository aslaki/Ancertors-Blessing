using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{

    public float lifeTimeSec = 5f;
    private Timer _lifeTimer = new Timer();
    private PlayerController playerController;

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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>()){
            playerController = collision.gameObject.GetComponent<PlayerController>();
            if(!playerController.isImmune)
            {
                playerController.TakeDamage();
            }
        }
        Destroy(gameObject);
    }
}
