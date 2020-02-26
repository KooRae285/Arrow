using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEffect : MonoBehaviour
{
    int Damage = 100;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerController _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (_Player.HitDamege(Damage))
            {
                _Player.curState = PlayerController.eMotion.HIT;
            }
            else
            {
                _Player.curState = PlayerController.eMotion.HIT;
            }
        }
    }
}
