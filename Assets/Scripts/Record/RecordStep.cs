using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RecordStep : MonoBehaviour
{
    
public enum RecordPlayerState
{
    player,
    box
}

    //定义一个存储位置和旋转的结构体
    public class TimeRecord
    {
        
        public Vector2 position;//位置
        public Quaternion rotation;//旋转
        public RecordPlayerState state;

        //构造函数
        public TimeRecord(Vector2 position, Quaternion rotation,RecordPlayerState state)
        {
            this.position = position;
            this.rotation = rotation;
            this.state=state;
        }
    }

    private List<TimeRecord> timeRecordList=new List<TimeRecord>();
    
    [SerializeField]
    private float RecordTime=50f;
    [SerializeField]
    private GameObject[] playerPrefab;
    [SerializeField]
    private ChangeToBox changeToBox;
    private int Playerinder=0;
    private bool isRecording=false;


    private Player playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState=GetComponent<Player>();
        changeToBox=GetComponent<ChangeToBox>();

    }

    void Update(){
        // if(playerState.stateMachine.currentState!=playerState.idleState)
        // {
        //     StartRecord();
        // }else {
        //     StopRecord();
        // }
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartRecord();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            StopRecord();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRecording)
        {
            Record();
        }else {
            Reward();
        }
    }
    private void StartRecord()
    {
        isRecording=true;
    }
    private void StopRecord()
    {
        isRecording=false;
    }
    //记录
    private void Record()
    {
        //记录位置和旋转 移除多余的记录
        // if(timeRecordList.Count>Mathf.Round(RecordTime/Time.deltaTime)){
        //    timeRecordList.RemoveAt(timeRecordList.Count-1);
        // }
        if(changeToBox.isBox){

         timeRecordList.Insert(0,new TimeRecord(transform.position,transform.rotation,RecordPlayerState.box));
        }
        else
         timeRecordList.Insert(0,new TimeRecord(transform.position,transform.rotation,RecordPlayerState.player));
    }
    //回放
    private void Reward(){
        if(timeRecordList.Count>0){
            //设置位置和旋转
            TimeRecord info=timeRecordList[0];
            
            Instantiate(playerPrefab[(int)info.state], info.position, info.rotation);
            timeRecordList.RemoveAt(0);
        }
    }
}
