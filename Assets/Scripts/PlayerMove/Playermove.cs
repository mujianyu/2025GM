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
    private enum MovementState { Idle, Run, Jump };//�ƶ�״̬

    [SerializeField] private LayerMask jumpableGround;// ����Ծ�ĵ���
    [Header("�ƶ��ٶ�")]
    [SerializeField] private float moveSpeed = 7f;
    [Header("��Ծ��")]
    [SerializeField] private float jumpForce = 7f;
    [Header("��Ч")]
    [SerializeField] private AudioSource jumpSoundEffect;

   
     
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //�����ƶ�����
        dirX = Input.GetAxisRaw("Horizontal");
        //�ƶ��ٶ�(ά��y����ٶ�)
     
         rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);//�ĳɸ�һ��˲����

        //���°�ť���ǵ���
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {   
            //������Ծ������
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