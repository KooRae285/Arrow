using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject CameraTarget;
    //GameObject Player;
    //JSController JS;
    // Start is called before the first frame update
    private void Awake()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        //JS = GameObject.FindGameObjectWithTag("JS").GetComponent<JSController>();
    }
    void Start()
    {
        transform.SetParent(CameraTarget.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CameraMoving()
    {
        
    }
}
