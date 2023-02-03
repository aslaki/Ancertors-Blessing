using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isUpPressed;
    private bool isDownPressed;
    private bool isLeftPressed;
    private bool isRightPressed;

    public Rigidbody2D body;
    public Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isUpPressed = Input.GetKey(KeyCode.UpArrow);
        isDownPressed = Input.GetKey(KeyCode.DownArrow);
        isLeftPressed = Input.GetKey(KeyCode.LeftArrow);
        isRightPressed = Input.GetKey(KeyCode.RightArrow);
    }


    private void FixedUpdate()
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
    }
}
