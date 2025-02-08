using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartReplay : MonoBehaviour
{
    private Button StartRecord;


    public ALLAction ALLactionBase;
    public int gameID;
    public GameObject Replayer;
    
    private void Start()
    {
        StartRecord = GetComponent<Button>();
        StartRecord.onClick.AddListener(StartR);
    }
    private void StartR()
    {

        Replayer.SetActive(true);
    }
}
