using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    int HitDamage;
    int WeaponDamage;
    int RandDamage;
    public int FinalDamage
    {
        get
        {
            return RandDamage;
        }
        set
        {
            RandDamage = value;
        }
    }
    public int ViewDamage
    {
        get
        {
            return HitDamage;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        WeaponDamage = Arrow._instance.MagicDamage;
        RandDamage = Random.Range((int)(0.9 * WeaponDamage), (int)(1.1 * WeaponDamage));
        HitDamage = RandDamage;
        Arrow._instance.MagicDamage = HitDamage;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
