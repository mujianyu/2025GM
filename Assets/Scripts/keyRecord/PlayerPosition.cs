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
        public Vector3 scale;
        public PlayerState playerState;
        public float time;
        public Pos(Vector3 position, Vector3 rotation,Vector3 scale,PlayerState playerState, float time)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.playerState = playerState;
            this.time = time;
        }
    }
    public void clear()
    {
        pos.Clear();
    }

    public List<Pos> pos=new List<Pos>(); 
}
