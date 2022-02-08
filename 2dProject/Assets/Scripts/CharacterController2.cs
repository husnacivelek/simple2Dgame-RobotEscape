using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2 : MonoBehaviour
{
    public float jumpForce = 150.0f;
    public float speed = 1.0f;
    private float moveDirection;
    private bool jump;
    private bool grounded = true;
    private bool moving;
    private Rigidbody2D _rigidbody2d;
    private Animator anim;
    private SpriteRenderer _spriteRenderer;

    void Awake(){
        anim = GetComponent<Animator>();
    }
    
    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate(){
        if (_rigidbody2d.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        _rigidbody2d.velocity = new Vector2(speed * moveDirection, _rigidbody2d.velocity.y);

        if (jump == true)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpForce);
            jump = false;
        }
    }

    void Update()
    {

        if (grounded == true && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed",speed);
            }else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = 1.0f;
                _spriteRenderer.flipX = false;
                anim.SetFloat("speed",speed);
            }
            

        }else if (grounded == true){
            moveDirection = 0.0f;
            anim.SetFloat("speed",0.0f);
        }

        if (grounded == true && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D other){

        
        if(other.gameObject.CompareTag("Zemin")){
            anim.SetBool("grounded", true);
            grounded = true;
        }
     

    }
}
