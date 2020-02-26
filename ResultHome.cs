using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultHome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickButton()
    {
        BaseManager._instance.StartLobbyScene("InGameScene");
        Time.timeScale = 1.0f;
    }
}
