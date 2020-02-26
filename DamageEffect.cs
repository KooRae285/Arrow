using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] Text Damage;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        Damage.text = Arrow._instance.MagicDamage.ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
