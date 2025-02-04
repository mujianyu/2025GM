using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CapsuleCollider2D))]


[RequireComponent(typeof(AudioSource))]
//脚本移动
public class Playermove : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    public SpriteRenderer sprite;
    public Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { Idle, Run, Jump }

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();

     
    }

    // Update is called once per frame
    private void Update()
    {
        //左右移动方向
        dirX = Input.GetAxisRaw("Horizontal");
        //移动速度
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {   
            //播放跳跃的音乐
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.Run;
            //sprite.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            state = MovementState.Run;
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            state = MovementState.Idle;
        }
        
        if (!IsGrounded())
        {
            state = MovementState.Jump;
        }
        
        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}