using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RercordAction : MonoBehaviour
{
    public ActionBase actionBase;
    public bool RecordStart=false;
    private void Update()
    {
        if(!RecordStart)
        {
            return;
        }
        List<Action.RecordState> state=new List<Action.RecordState>();
        float time=Time.time;
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightShift)|| Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
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

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                state.Add(Action.RecordState.Tran);
            }


            // 检查上一个 Action 的 state 是否与当前相同
            if (actionBase.actions.Count > 0)
            {
                // 获取 actions 列表中的最后一个 Action
                Action lastAction = actionBase.actions[actionBase.actions.Count - 1];

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
            }else{
                actionBase.actions.Add(new Action(state, time));
            }
        }
            

    }

}
