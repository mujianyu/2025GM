using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerAction", menuName = "ScriptableObject/�����Ϊ", order = 0)]
[System.Serializable]
public class ActionBase : ScriptableObject
{
    public float startTime;
    public List<Action> actions = new List<Action>();
    public void clear()
    {
        actions.Clear();
    }

}
