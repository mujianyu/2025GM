using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayRecord : MonoBehaviour
{
    public PlayerPosition playerPosition;
    public float startTime;
    public float currentTime;
    public Animator anim;
    private int index=0;
    
    private void Start()
    {
        startTime = Time.time;
        currentTime = Time.time;
    }

    private void FixedUpdate()
    {
        currentTime +=Time.fixedDeltaTime;
        if (playerPosition.pos.Count > index)
        {
            if (currentTime - startTime > playerPosition.pos[index].time)
            {
                transform.position = playerPosition.pos[index].position;
                transform.eulerAngles = playerPosition.pos[index].rotation;
                transform.localScale = playerPosition.pos[index].scale;
                anim.SetInteger("ReplayState", (int)playerPosition.pos[index].playerState);
                index++;
            }
            else return;
        }
    }

}
