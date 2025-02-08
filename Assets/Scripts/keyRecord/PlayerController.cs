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
    [Header("�ƶ��ٶ�")]
    [SerializeField]
    private float moveSpeed = 7f;
    [Header("��Ծ��")]
    [SerializeField]
    private float jumpForce = 14f;
    [Header("��Ծ��Ч")]
    [SerializeField]
    private AudioSource jumpSoundEffect;
    [Header("��Ħ��")]
    public PhysicsMaterial2D p1;
    [Header("��Ħ��")]
    public PhysicsMaterial2D p2;
    private bool isbox = false;
    private float pregravity;
    private Vector2 playerSize;
    private Vector2 boxSize;
    private string playerlayer = "Player";
    private string boxlayer = "Box";
    public PlayerPosition playerPosition;

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

        //��ȡ�任box����
        HandleChangeInput();
        if (isbox)
        {
            return;
        }
        // ��ȡˮƽ����
        HandleMovementInput();

        // ��ȡ��Ծ����
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        if (isbox)
        {
            //��ֹ������ƶ�����Ծ
            coll.size = boxSize;
            this.gameObject.layer = LayerMask.NameToLayer(boxlayer);

            changeBox();
        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer(playerlayer);

            coll.size = playerSize;
            //�ָ�����
            rb.gravityScale = pregravity;
            // �����ɫ�������ƶ�
            Move();

            // ������Ծ��������ĸ��£�
            if (isJumping && IsGrounded())
            {
                Jump();
            }


            // �����Ƿ��ڵ����ϣ��л�����
            if (IsGrounded())
            {
                rb.sharedMaterial = p1;
            }
            else
            {
                rb.sharedMaterial = p2;
            }
        }

        // ���¶���״̬
        UpdateAnimationState();
    }
    private void changeBox()
    {
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
    }
    // ����ˮƽ�ƶ�����
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            dirX = 0f;
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dirX = -1f; // A �����£�ֵΪ -1
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dirX = 1f;  // D �����£�ֵΪ 1
        }
        else
        {
            dirX = 0f;  // û�а�����ˮƽ�ٶ�Ϊ 0
        }


    }
    //����任box
    private void HandleChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            isbox = !isbox;
        }


    }

    // ������Ծ����
    private void HandleJumpInput()
    {
        // �����Ծ����
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true; // ������Ծ��־
        }
    }

    // ��������ˮƽ�ƶ�
    private void Move()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // ���¸����ˮƽ�ٶ�
    }

    // ִ����Ծ����
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ��Ծʱ�ı䴹ֱ�ٶ�
        isJumping = false; // ��Ծ��������Ծ��־
        jumpSoundEffect.Play();//������Ծ��Ч
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
        //����
        playerPosition.pos.Add(new PlayerPosition.Pos(transform.position, transform.eulerAngles, (PlayerPosition.PlayerState)state, Time.time));
        anim.SetInteger("state", (int)state);
    }
 
    private bool IsGrounded()
    {
        Vector2 size = new Vector2(coll.bounds.size.x / 2, coll.bounds.size.y);
        Vector2 pos = new Vector2(coll.bounds.center.x, coll.bounds.center.y - coll.bounds.size.y / 2);
        return Physics2D.BoxCast(pos, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
