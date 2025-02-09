using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartReplay : MonoBehaviour
{
    private Button StartRecord;
    public GameObject Replayer;
    public Transform start;
    
    private void Start()
    {
        StartRecord = GetComponent<Button>();
        StartRecord.onClick.AddListener(StartR);
    }
    private void StartR()
    {
        Replayer.transform.position = start.position;   
        Replayer.SetActive(true);
    }
}
