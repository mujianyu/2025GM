using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Replaymove : MonoBehaviour
{
    public int scene;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    public Animator anim;
    

    private float dirX = 0f;
    private float StartTime= 0f;
    private float CurrentTime = 0f;
    private enum MovementState { Idle, Run, Jump ,Box};
    public ActionBase actionBase;


    [SerializeField] 
    private LayerMask jumpableGround;
    [Header("移动速度")]
    [SerializeField] 
    private float moveSpeed = 7f;
    [Header("跳跃")]
    [SerializeField] 
    private float jumpForce = 7f;

    private MovementState state;
    private bool isbox = false;
    private float pregravity;
    private string ReplayPlayerlayer = "ReplayPlayer";
    private string boxlayer = "Box";
    private Vector2 ReplayerSize;
    private Vector2 boxSize;
    private bool startPlay = false;
    private int index = 0;
    private float firstStartTime= 0f;
    private float startTime;


    private void Start()
    {
       actionBase= GameObject.Find("GameData").GetComponent<single>().t[scene];
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        CurrentTime = Time.time;
        pregravity = rb.gravityScale;
        ReplayerSize = coll.size;
        boxSize = new Vector2(1f, 1f);
        startTime = Time.time;
       
        index = 0;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {

        CurrentTime = Time.time - startTime;
 

        if (index < actionBase.actions.Count)
        {
            List<Action> canActions = new List<Action>();

            //处理快速移动，将当前时间点所有的 action 添加到 canActions 中
            for(int i=index;i<actionBase.actions.Count; i++)
            {
                if(CurrentTime >= actionBase.actions[i].time)
                {
                    canActions.Add(actionBase.actions[i]);
                }else{
                    break;
                }
            }


            List<Action.RecordState> combinedState = new List<Action.RecordState>();



            //所有能够执行的 action ，有跳跃就添加跳跃，有变换就添加变化。(融合，只考虑最新帧的方向)
            // jump(无)/left jump/ rightg jump
            for (int i = 0; i < canActions.Count; i++)
            {


                if (canActions[i].state.Contains(Action.RecordState.Jump))
                {
                    combinedState.Add(Action.RecordState.Jump);
                }

                if (canActions[i].state.Contains(Action.RecordState.Tran))
                {
                    //combinedState.Add(Action.RecordState.Tran);
                    isbox = !isbox;
                }

                if (i == canActions.Count - 1)
                {
                    if (canActions[i].state.Contains(Action.RecordState.Left))
                        combinedState.Add(Action.RecordState.Left);
                    if (canActions[i].state.Contains(Action.RecordState.Right))
                        combinedState.Add(Action.RecordState.Right);
                }


                //actionBase.actions.RemoveAt(0);
                index++;
            }

            //处理变换，得到变化的次数
            //int tranCount = combinedState.FindAll(s => s == Action.RecordState.Tran).Count;
            //如果变换次数为奇数，就添加变换
            //tranCount %= 2;
            //combinedState.RemoveAll(s => s == Action.RecordState.Tran);
            //if(tranCount >= 1)
            //{
            //    //combinedState.Add(Action.RecordState.Tran);
            //    isbox = true;
            //}else isbox = false;

            if (isbox)
            {
                rb.velocity = new Vector2(0, 0);
                rb.gravityScale = 0;
                coll.size = boxSize;
                this.gameObject.layer = LayerMask.NameToLayer(boxlayer);
                UpdateAnimationState();
                return;
            }
            else
            {
                this.gameObject.layer = LayerMask.NameToLayer(ReplayPlayerlayer);
                coll.size = ReplayerSize;
                rb.gravityScale = pregravity;
            }
            //最后一个键为空(松开)，用空键来确定结束的时间
            //canActions最后一个键为空，此前一直维持上一个的状态(else执行，前一个状态dirX)
            if (canActions.Count >0)
            {
                //如果有左或者右按键，就设置方向
                if (combinedState.Contains(Action.RecordState.Left) )
                {
                    dirX = -1.0f;
                }
                if(combinedState.Contains(Action.RecordState.Right) )
                {
                    dirX = 1.0f;
                }
                //如果左右按键都有，就不移动
                if ( combinedState.Contains(Action.RecordState.Right) && combinedState.Contains(Action.RecordState.Left))
                {
                    dirX = 0f;
                }

                //如果左右按键都没有，就不移动
                if ( !combinedState.Contains(Action.RecordState.Right) && !combinedState.Contains(Action.RecordState.Left))
                {
                    dirX = 0f;
                }
                //更新水平速度
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
                //如果有跳跃按键，且在地面上，就跳跃
                if (combinedState.Contains( Action.RecordState.Jump) && IsGrounded())
                {
                    //更新垂直速度
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                
            }
            else
            {
                //当前时间点没有 action ，维持原来的速度
                //因为有摩擦力，所以速度会逐渐减小
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            }
    


        }
        else{
            //没有 action ，不移动
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        

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
        if(isbox)
        {
            state = MovementState.Box;
        }

        anim.SetInteger("ReplayState", (int)state);
    }
    private bool IsGrounded()
    {
        Vector2 size = new Vector2(coll.bounds.size.x / 2, 0.2f);
        Vector2 pos = new Vector2(coll.bounds.center.x, coll.bounds.center.y - coll.bounds.size.y / 2 + 0.1f);
        return Physics2D.BoxCast(pos, size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}