using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ALLPos", menuName = "ScriptableObject/����λ��", order = 3)]
public class ALLPos : ScriptableObject
{
    public List<PlayerPosition> actionBases = new List<PlayerPosition>();
}
