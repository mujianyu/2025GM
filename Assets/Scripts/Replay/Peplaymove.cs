using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AudioSource))]
//��ɫ����
public class Peplaymove : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    public SpriteRenderer sprite;
    public Animator anim;
    private float dirX = 0f;//����
    private float StartTime= 0f;
    private float CurrentTime = 0f;
    private enum MovementState { Idle, Run, Jump };//�ƶ�״̬
    public ActionBase actionBase;


    private float preDirX=0f;
    [SerializeField] private LayerMask jumpableGround;// ����Ծ�ĵ���
    [Header("�ƶ��ٶ�")]
    [SerializeField] private float moveSpeed = 7f;
    [Header("��Ծ��")]
    [SerializeField] private float jumpForce = 7f;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        StartTime = Time.time;
        CurrentTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        //�����ƶ�����
        //dirX = Input.GetAxisRaw("Horizontal");

        //�ƶ��ٶ�(ά��y����ٶ�)
        dirX= 0f;
        CurrentTime = Time.time - StartTime;

        if (actionBase.actions.Count > 0)
        {
            if(CurrentTime >= actionBase.actions[0].time)
            {
                if (actionBase.actions[0].state[0] == Action.RecordState.Left_Start )
                    dirX = -1.0f;
                else if(actionBase.actions[0].state[0] == Action.RecordState.Right_Start)
                    dirX = 1.0f;
                else if(actionBase.actions[0].state[0] == Action.RecordState.Left_End)
                    dirX = 0.0f;
                else  if (actionBase.actions[0].state[0] == Action.RecordState.Right_End)
                    dirX = 0.0f;
                if (actionBase.actions[0].state[0] == Action.RecordState.Jump_Start)
                {
                    //ǰһ���ķ���
                    rb.velocity = new Vector2(preDirX * moveSpeed, jumpForce);
                    
                }else
                {
                    preDirX = dirX;
                    rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
                }
                
                actionBase.actions.RemoveAt(0);
            }
            else
            {
                dirX = preDirX;
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            }
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

        anim.SetInteger("ReplayState", (int)state);
    }
    private bool IsGrounded()
    {
        //public static RaycastHit2D BoxCast (Vector2 origin, Vector2 size, float angle, Vector2 direction,
        //float distance= Mathf.Infinity, int layerMask= Physics2D.AllLayers, float minDepth= -Mathf.Infinity, float maxDepth= Mathf.Infinity);
        // �϶�һ����СΪcoll�ĺ���(���£�distance=0.1f)������Ƿ�����ײ
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}