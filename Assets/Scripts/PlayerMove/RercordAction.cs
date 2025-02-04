using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RercordAction : MonoBehaviour
{
    public ActionBase actionBase;

    private void Update()
    {
        List<Action.RecordState> state=new List<Action.RecordState>();
        float time=Time.time;
        if (Input.GetKeyDown(KeyCode.A))
        {
            state.Add(Action.RecordState.Left_Start);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            state.Add(Action.RecordState.Right_Start);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.Add(Action.RecordState.Jump_Start);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            state.Add(Action.RecordState.Left_End);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            state.Add(Action.RecordState.Right_End);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            state.Add(Action.RecordState.Jump_End);
        }

        for (int i = 0; i < state.Count; i++)
        {
            actionBase.actions.Add(new Action(state,time));
        }

    }

}
