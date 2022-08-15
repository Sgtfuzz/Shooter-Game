using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
[SerializeField] float spellSpeed = 20f;
Rigidbody2D myRigidbody;
PlayerController player;
float xSpeed;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        xSpeed = player.transform.localScale.x * spellSpeed;
        Destroy (gameObject, 2f);
    }

 
    void Update()
    {
        myRigidbody.velocity = new Vector2 (xSpeed, 0f);        
    }
}
