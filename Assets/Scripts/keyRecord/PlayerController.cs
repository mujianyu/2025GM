using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    public Animator anim;
    private float dirX = 0f;
    private bool isJumping = false;
    private enum MovementState { Idle, Run, Jump, Box };

    [SerializeField]
    private LayerMask jumpableGround;
    [Header("移动速度")]
    [SerializeField]
    private float moveSpeed = 7f;
    [Header("跳跃力")]
    [SerializeField]
    private float jumpForce = 14f;
    [Header("跳跃音效")]
    [SerializeField]
    private AudioSource jumpSoundEffect;
    [Header("有摩擦")]
    public PhysicsMaterial2D p1;
    [Header("无摩擦")]
    public PhysicsMaterial2D p2;
    private bool isbox = false;
    private float pregravity;
    private Vector2 playerSize;
    private Vector2 boxSize;
    private string playerlayer = "Player";
    private string boxlayer = "Box";
    public PlayerPosition playerPosition;
    private bool startRecord = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rb.sharedMaterial = p1;
        pregravity = rb.gravityScale;
        playerSize = coll.size;
        boxSize = new Vector2(1f, 1f);
    }

    void Update()
    {

        //获取变换box输入
        HandleChangeInput();
        if (isbox)
        {
            return;
        }
        // 获取水平输入
        HandleMovementInput();

        // 获取跳跃输入
        HandleJumpInput();
    }

    private void FixedUpdate()
    {

        if (isbox)
        {
            //静止物体的移动和跳跃
            coll.size = boxSize;
            this.gameObject.layer = LayerMask.NameToLayer(boxlayer);

            changeBox();
        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer(playerlayer);

            coll.size = playerSize;
            //恢复重力
            rb.gravityScale = pregravity;
            // 处理角色的物理移动
            Move();

            // 处理跳跃（物理方面的更新）
            if (isJumping && IsGrounded())
            {
                Jump();
            }


            // 根据是否在地面上，切换材质
            if (IsGrounded())
            {
                rb.sharedMaterial = p1;
            }
            else
            {
                
                rb.sharedMaterial = p2;
            }
        }

        // 更新动画状态
        UpdateAnimationState();
    }
    private void changeBox()
    {
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
    }
    // 处理水平移动输入
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            dirX = 0f;
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dirX = -1f; // A 键按下，值为 -1
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dirX = 1f;  // D 键按下，值为 1
        }
        else
        {
            dirX = 0f;  // 没有按键，水平速度为 0
        }


    }
    //处理变换box
    private void HandleChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            isbox = !isbox;
        }


    }

    // 处理跳跃输入
    private void HandleJumpInput()
    {
        // 检测跳跃按键
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true; // 设置跳跃标志
        }
    }

    // 处理物理水平移动
    private void Move()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // 更新刚体的水平速度
    }

    // 执行跳跃动作
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 跳跃时改变垂直速度
        isJumping = false; // 跳跃后重置跳跃标志
        jumpSoundEffect.Play();//播放跳跃音效
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.Run;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            state = MovementState.Run;
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

        if (isbox)
        {
            state = MovementState.Box;
        }
        //保存
        
        playerPosition.pos.Add(new PlayerPosition.Pos(transform.position, transform.eulerAngles,transform.localScale, (PlayerPosition.PlayerState)state, Time.time));
        anim.SetInteger("state", (int)state);
    }
 
    private bool IsGrounded()
    {
        Vector2 size = new Vector2(coll.bounds.size.x / 2, 0.1f);
        Vector2 pos = new Vector2(coll.bounds.center.x, coll.bounds.center.y - coll.bounds.size.y / 2);
        return Physics2D.BoxCast(pos, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
