using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;



public class RercordAction : MonoBehaviour
{
    public ActionBase actionBase;
    public bool RecordStart = false;
    private bool isbox=false;
    private bool isboxstateChange = false;//第一次变成box状态
    private float currentTime;
    private float startTime=0f;
    
    private void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {

        //if (!RecordStart)
        //{
        //    return;
        //}

        List<Action.RecordState> state = new List<Action.RecordState>();
        float time = Time.time-startTime;
            
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if(startTime== 0)
            {
                actionBase.startTime=time;
                startTime = time;
            }

            if (Input.GetKey(KeyCode.A))
            {
                state.Add(Action.RecordState.Left);
            }

            if (Input.GetKey(KeyCode.D))
            {
                state.Add(Action.RecordState.Right);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                state.Add(Action.RecordState.Jump);
            }
            isboxstateChange = false;
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                isbox = !isbox;
                isboxstateChange = true;
                state.Add(Action.RecordState.Tran);
            }
            //当前如果是box状态不接受输入(除了RightShift)
            //存储变成box的状态
            if (isbox)
            {
                if (isboxstateChange)
                {
                    actionBase.actions.Add(new Action(state, time));
                }
                return;
            }
            
            // 检查上一个 Action 的 state 是否与当前相同
            if (actionBase.actions.Count > 0)
            {
                // 获取 actions 列表中的最后一个 Action
                Action lastAction = actionBase.actions[actionBase.actions.Count - 1];

                if (time - lastAction.time > 0.05f)
                {
                    actionBase.actions.Add(new Action(state, time));
                    return;
                }
                // 比较 lastAction.state 和当前 state 是否相同
                bool isStateEqual = true;
                if (lastAction.state.Count != state.Count)
                {
                    isStateEqual = false;  // 如果状态数量不同，认为不相同
                }
                else
                {
                    for (int i = 0; i < lastAction.state.Count; i++)
                    {
                        if (!lastAction.state[i].Equals(state[i]))
                        {
                            isStateEqual = false;  // 如果有任何不相同的元素，认为不相同
                            break;
                        }
                    }
                }

                // 如果不相同，才添加新的 Action
                if (!isStateEqual)
                {
                    actionBase.actions.Add(new Action(state, time));
                }
            }
            else
            {
                actionBase.actions.Add(new Action(state, time));
            }
        }


    }

}