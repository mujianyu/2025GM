using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ReplayRecord : MonoBehaviour
{
    public PlayerPosition playerPosition;
    public Transform player;
    public float startTime;
    public float currentTime;
    public Animator anim;
    private int index=0;
    public CinemachineVirtualCamera cinemachine;
    private void OnEnable()
    {
        index = 0;
        startTime = Time.time;
        currentTime = Time.time;
        cinemachine.Follow=this.transform;
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
            else
            {
                cinemachine.Follow = player;
                return;
            };
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            cinemachine.Follow = player;

            this.gameObject.SetActive(false);
        }
    }
    
}
