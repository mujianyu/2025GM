using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class JsonHandler : MonoBehaviour
{

    public ActionBase actionBase;
    public ActionBase actionBase2;
   public void Awake()
    {
        string jsonStr = JsonUtility.ToJson(actionBase);
        File.WriteAllText(Application.persistentDataPath + "/MrTang.json", jsonStr);

        
        jsonStr = File.ReadAllText(Application.persistentDataPath + "/MrTang.json");
        //ActionBase actionsData = JsonSerializer.Deserialize<ActionBase>(jsonStr);
        Debug.Log(jsonStr);
        //actionBase2=new ActionBase();
        //ʹ��Json�ַ������� ת���������

        //var t=JsonUtility.FromJson<ActionBase>(jsonStr);

        //actionBase2 = JsonUtility.FromJson(jsonStr, typeof(ActionBase)) as ActionBase;


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
