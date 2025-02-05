using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPosition", menuName = "ScriptableObject/ÕÊº“Œª÷√", order = 1)]
public class PlayerPosition : ScriptableObject
{
    [System.Serializable]
    public class Pos
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    public List<Pos> pos=new List<Pos>(); 
}
