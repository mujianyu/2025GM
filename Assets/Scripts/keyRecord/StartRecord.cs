using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartRecord : MonoBehaviour
{
    public PlayerPosition playerPosition;
    private Button Record;
    private bool statr = false;

    private void Start()
    {
        Record = GetComponent<Button>();
        Record.onClick.AddListener(StartR);
    }
    private void StartR()
    {
        if(statr)
        {
            statr = false;
        }
        else
        {
            statr = true;
            playerPosition.pos.Clear();
        }
    }
}
