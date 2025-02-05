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
    private void FixedUpdate()
    {
        //�����ƶ�����
        //dirX = Input.GetAxisRaw("Horizontal");
        //�ƶ��ٶ�(ά��y����ٶ�)
        CurrentTime = Time.time - StartTime;

        if (actionBase.actions.Count > 0)
        {
            List<Action> canActions = new List<Action>();

            foreach (var action in actionBase.actions)
            {
                if(CurrentTime >=action.time)
                {
                    canActions.Add(action);
                    
                }else{
                    break;
                }
            }


            List<Action.RecordState> combinedState = new List<Action.RecordState>();
            for (int i = 0; i < canActions.Count; i++)
            {
                
                if(canActions[i].state.Contains(Action.RecordState.Jump))
                {
                    combinedState.Add(Action.RecordState.Jump);
                }

                if(canActions[i].state.Contains(Action.RecordState.Tran))
                {
                    combinedState.Add(Action.RecordState.Tran);
                }

                if(i == canActions.Count-1)
                {
                    if(canActions[i].state.Contains(Action.RecordState.Left))
                        combinedState.Add(Action.RecordState.Left);
                    if(canActions[i].state.Contains(Action.RecordState.Right))
                        combinedState.Add(Action.RecordState.Right); 
                }

                actionBase.actions.RemoveAt(0);
            }

            int tranCount = combinedState.FindAll(s => s == Action.RecordState.Tran).Count;
            tranCount %= 2;
            combinedState.RemoveAll(s => s == Action.RecordState.Tran);
            if(tranCount == 1)
            {
                combinedState.Add(Action.RecordState.Tran);
            }


            if(canActions.Count >0)
            {
                
                // 使用 string.Join 将数组的值连接为一个字符串
                Debug.Log(string.Join(", ", combinedState));

                if (combinedState.Contains(Action.RecordState.Left) )
                {
                    dirX = -1.0f;
                }
                if(combinedState.Contains(Action.RecordState.Right) )
                {
                    dirX = 1.0f;
                }
                
                if( combinedState.Contains(Action.RecordState.Right) && combinedState.Contains(Action.RecordState.Left))
                {
                    dirX = 0f;
                }
                

                if( !combinedState.Contains(Action.RecordState.Right) && !combinedState.Contains(Action.RecordState.Left))
                {
                    dirX = 0f;
                }

                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

                if (combinedState.Contains( Action.RecordState.Jump) && IsGrounded())
                {
                    
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    
                }

                // if(actionBase.actions.Count == 0)
                // {
                //     actionBase.actions.Add
                // }

                
            }else
            {   
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            }

            
        }else{
            dirX = 0f;
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
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