using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action 
{
    public enum RecordState { Left, Right, Jump,Tran };
    
    public List<RecordState> state;
    public float time;

    public Action(List<RecordState> state,float time)
    {
        this.state = state;
        this.time = time;
    }
}
