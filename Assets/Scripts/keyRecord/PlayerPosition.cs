using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPosition", menuName = "ScriptableObject/ÕÊº“Œª÷√", order = 1)]
public class PlayerPosition : ScriptableObject
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        box
    }
    [System.Serializable]
    public class Pos
    {
        public Vector3 position;
        public Vector3 rotation;
        public PlayerState playerState;
        public float time;
        public Pos(Vector3 position, Vector3 rotation, PlayerState playerState, float time)
        {
            this.position = position;
            this.rotation = rotation;
            this.playerState = playerState;
            this.time = time;
        }
    }

    public List<Pos> pos=new List<Pos>(); 
}
