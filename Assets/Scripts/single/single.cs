using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class single : MonoBehaviour
{

    public List<ActionBase> t;


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}

