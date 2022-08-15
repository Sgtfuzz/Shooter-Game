using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaser : MonoBehaviour
{
    float moveSpeed = 15f;
    Rigidbody2D rb;
    PlayerController target;
    Vector2 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerController>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        Destroy (gameObject, 3f);
    }


    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Ground")        
        {
            Destroy(gameObject);
        }    
    }

}
