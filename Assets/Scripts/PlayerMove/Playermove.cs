using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AudioSource))]
//��ɫ����
public class Playermove : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    public SpriteRenderer sprite;
    public Animator anim;
    private float dirX = 0f;//����
    private bool isJumping = false;
    private enum MovementState { Idle, Run, Jump };//�ƶ�״̬

    [SerializeField] private LayerMask jumpableGround;// ����Ծ�ĵ���
    [Header("�ƶ��ٶ�")]
    [SerializeField] private float moveSpeed = 7f;
    [Header("��Ծ��")]
    [SerializeField] private float jumpForce = 7f;
    [Header("��Ч")]
    [SerializeField] private AudioSource jumpSoundEffect;
    [Header("��Ħ��")]
    public PhysicsMaterial2D p1;    // ��Ħ������
    [Header("��Ħ��")]
    public PhysicsMaterial2D p2;    // ��Ħ������


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        rb.sharedMaterial = p1;
    }

    void Update()
    {
        // 获取水平输入
        HandleMovementInput();

        // 获取跳跃输入
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        // 处理角色的物理移动
        Move();

        // 处理跳跃（物理方面的更新）
        if (isJumping && IsGrounded())
        {
            Jump();
        }

        // 更新动画状态
        UpdateAnimationState();
    }

    // 处理水平移动输入
    private void HandleMovementInput()
    {
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
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
        {   
            //������Ծ������
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (IsGrounded())
        {
            rb.sharedMaterial = p1;
        }else
        {
            rb.sharedMaterial = p2;
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.Run;
            // transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            state = MovementState.Run;
            // transform.localScale = new Vector3(-1, 1, 1);
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
        //public static RaycastHit2D BoxCast (Vector2 origin, Vector2 size, float angle, Vector2 direction,
        //float distance= Mathf.Infinity, int layerMask= Physics2D.AllLayers, float minDepth= -Mathf.Infinity, float maxDepth= Mathf.Infinity);
        // �϶�һ����СΪcoll�ĺ���(���£�distance=0.1f)������Ƿ�����ײ
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}