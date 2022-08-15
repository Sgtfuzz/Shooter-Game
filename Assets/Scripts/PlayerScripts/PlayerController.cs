using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

[Header("Movement")]
[SerializeField] float moveSpeed = 1f;
[SerializeField] float jumpSpeed = 5f;
[Header("Phase")]
bool isPhasing;
Renderer rend;
Color fade;
[SerializeField] GameObject spell;
[SerializeField] Transform staff;

Vector2 moveInput;
Rigidbody2D myRigidbody;
Animator myAnimator;
BoxCollider2D myFeetCollider;


    void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        fade = rend.material.color;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        Move();
        FlipSprite();
    }

//print player movement input to console log.
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }


//Move player sprite left or right
    void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

//Flip character sprite left/right and maintain that facing
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
//Prvents player from hitting jump multiple times and calculates jump speed.
    void OnJump(InputValue value)
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}

        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if(value.isPressed)
        {
            StartCoroutine("Shooting");
        }

    }

   void OnPhase(InputValue value)
    {
        if(value.isPressed)
        {
            StartCoroutine("Phasing");
        }

    }

    IEnumerator Phasing()
    {
        Physics2D.IgnoreLayerCollision(7, 10, true);
        fade.a = 0.5f;
        rend.material.color = fade;
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(7, 10, false);
        fade.a = 1f;
        rend.material.color = fade;
    }

    IEnumerator Shooting()
    {
        myAnimator.SetBool("isShooting", true);
        yield return new WaitForSeconds(0.26f);
        Instantiate(spell, staff.position, Quaternion.identity);
        myAnimator.SetBool("isShooting", false);
    }

}
