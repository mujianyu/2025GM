using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerAction", menuName = "ScriptableObject/�����Ϊ", order = 0)]
public class ActionBase : ScriptableObject
{
    
    public List<Action> actions = new List<Action>();
}
