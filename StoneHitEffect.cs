using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHitEffect : MonoBehaviour
{
    [SerializeField] GameObject FinalEffect;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            GameObject go = Instantiate(FinalEffect, GameObject.FindGameObjectWithTag("InGameObject").transform);
            go.transform.position = transform.position;
        }
    }
}
