using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    private Button StartRecord;
    
    
    public GameObject ReplayPlayer;
    private void Start()
    {
        StartRecord = GetComponent<Button>();
        StartRecord.onClick.AddListener(ReplayRecord);
    }
    private void ReplayRecord()
    {
        ReplayPlayer.SetActive(true);
    }

}
