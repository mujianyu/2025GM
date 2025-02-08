using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayRecord : MonoBehaviour
{
    public PlayerPosition playerPosition;
    public float startTime;
    public float currentTime;
    public Animator anim;
    private void Start()
    {
        startTime = Time.time;
        currentTime = Time.time;
    }

    private void FixedUpdate()
    {
        currentTime +=Time.fixedDeltaTime;
        if (playerPosition.pos.Count > 0)
        {
            if (currentTime - startTime > playerPosition.pos[0].time)
            {
                transform.position = playerPosition.pos[0].position;
                transform.eulerAngles = playerPosition.pos[0].rotation;
                transform.localScale = playerPosition.pos[0].scale;
                anim.SetInteger("ReplayState", (int)playerPosition.pos[0].playerState);
                playerPosition.pos.RemoveAt(0);
            }
            else return;
        }
    }

}
