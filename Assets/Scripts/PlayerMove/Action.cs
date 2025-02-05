using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action 
{
    public enum RecordState { Left_Start, Left_End, Right_Start, Right_End, Jump_Start };
    public List<RecordState> state;
    public float time;
    public Action(List<RecordState> initialState,float time)
    {
        state = initialState;
        this.time = time;
    }
}
