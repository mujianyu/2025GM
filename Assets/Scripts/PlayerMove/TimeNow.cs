using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Time", menuName = "ScriptableObject/当前关卡开始时间", order = 4)]
public class TimeNow : ScriptableObject
{
    public List<float> time;
}
