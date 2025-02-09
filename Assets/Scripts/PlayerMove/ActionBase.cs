using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ALLAction", menuName = "ScriptableObject/所有动作", order = 0)]
[System.Serializable]
public class ActionBase : ScriptableObject
{
    public List<Action> actions ;
    //public void clear()
    //{
    //    actions.Clear();
    //}
}
