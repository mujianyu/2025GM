using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    private Button RestartB;
    public ActionBase actionBase;
    public PlayerPosition Pos;
    public Playermove playermove;
    public RercordAction action;
    public GameObject player;
    public GameObject ghost;
    public Transform start;

    private void Start()
    {
        RestartB = GetComponent<Button>();
        RestartB.onClick.AddListener(RestartR);
        
    }
    private void RestartR()
    {
        actionBase.actions.Clear();
        Pos.clear();
        action.setUpStartTime();
        playermove.startTime = Time.time;
        playermove.end = false;
        player.transform.position = start.position;
        ghost.transform.position = start.position;
        
    }

}
