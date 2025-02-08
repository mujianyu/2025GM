using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ALLAction", menuName = "ScriptableObject/所有行为", order = 2)]
public class ALLAction : ScriptableObject
{
    public List<ActionBase> actionBases = new List<ActionBase>();
}
