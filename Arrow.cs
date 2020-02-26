using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float _forceForward = 50;
    [SerializeField] float _timeLife = 5;
    [SerializeField] GameObject _HitEffect;
    [SerializeField] GameObject _DamageEffect;
    PlayerController _ownerPC;
    int _Damage = 500;
    Rigidbody _rgd3D;
    WeaponManager _thisweapon;
    Camera _maincamera;
    
    static Arrow _Uniqueinstance;
    public static Arrow _instance
    {
        get
        {
            return _Uniqueinstance;
        }
    }
    public int MagicDamage
    {
        get
        {
            return _Damage;
        }
        set
        {
            _Damage = value;
        }
    }

    private void Awake()
    {
        _Uniqueinstance = this;
        _ownerPC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _rgd3D = GetComponent<Rigidbody>();
        _thisweapon = GetComponent<WeaponManager>();
        _thisweapon.FinalDamage = _Damage;
        _maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rgd3D.AddForce(transform.forward * _forceForward, ForceMode.Impulse);
        Destroy(gameObject, _timeLife);
    }
    // Update is called once per frame
    void Update()
    {
       // Vector3.MoveTowards(transform.forward, _ownerPC.WeaponTarget.transform.position, 10);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            GameObject go = Instantiate(_HitEffect,GameObject.FindGameObjectWithTag("InGameObject").transform);
            go.transform.position = transform.position;
            Destroy(go, 0.8f);
            go = Instantiate(_DamageEffect, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
            Vector3 screenPos = _maincamera.WorldToScreenPoint(other.transform.position);
            go.transform.position = new Vector3(screenPos.x, screenPos.y, go.transform.position.z);
            Destroy(go, 2.0f);
            
            if (_ownerPC.ShotPiece)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if(other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        
    }
    //public void InitDamage(PlayerController pc)
    //{
    //    _ownerPC = pc;
    //    _Damage = pc._FinishATT;
    //}
    
}
