using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    private Button RestartB;

    public int sceneID = 1;
    public ALLAction ALLactionBase;
    public ALLPos aLLPos;
    public Playermove playermove;
    private void Start()
    {
        RestartB = GetComponent<Button>();
        RestartB.onClick.AddListener(RestartR);
    }
    private void RestartR()
    {
        ALLactionBase.actionBases[sceneID - 1].clear();
        aLLPos.actionBases[sceneID - 1].clear();
        playermove.setStartTime(Time.time);
        SceneManager.LoadScene(sceneID);
        
    }

}
