using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Replay : MonoBehaviour
{
    private Button StartRecord;
    
    private bool statr = false;
    public RercordAction Recordaction;
   
    public ActionBase actionBase;
    private void Start()
    {
        StartRecord = GetComponent<Button>();
        StartRecord.onClick.AddListener(StartR);
    }
    private void StartR()
    {
        if (statr)
        {
            statr = false;
            Recordaction.RecordStart = false;
        }
        else
        {
            statr = true;
            actionBase.clear();
            Recordaction.RecordStart = true;
        }
    }

}
