using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Status
{
    public enum eMotion
    {
        IDLE = 0,
        MOVE,
        ATTACK,
        HIT,
        DIE
    }
    [SerializeField] GameObject HPbar;
    [SerializeField] GameObject Weapon;
    [SerializeField] GameObject WeaponPos;
    [SerializeField] GameObject Side1;
    [SerializeField] GameObject Side2;
    [SerializeField] GameObject Back;
    [SerializeField] GameObject Resultwnd;
    [SerializeField] GameObject TargetRing;
    Slider HPui;
    Camera _maincamera;
    int ArrowCount = 1;
    public float AttackSpeed;
    JSController JS;
    Animator animator;
    eMotion _currentState;
    string _Name;
    float _time;
    bool _isDead;
    GameObject Target;
    GameObject[] Targets;
    GameObject Ring;
    SpawnCtrl spct;
    bool PieceShot = false;
    bool SideShot = false;
    bool BackShot = false;
    static PlayerController _UniqueInstance;
    public static PlayerController _instance
    {
        get
        {
            return _UniqueInstance;
        }
    }
    public eMotion curState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
        }
    }
    public GameObject WeaponTarget
    {
        get
        {
            return Target;
        }
    }
    public int CountArrow
    {
        get
        {
            return ArrowCount;
        }
        set
        {
            ArrowCount = value;
        }
    }
    public float _curHP
    {
        get
        {
            return _CurHP;
        }
        set
        {
            _CurHP = value;
        }
    }
    public float _maxHP
    {
        get
        {
            return _MaxHP;
        }
    }
    public bool ShotSide
    {
        get
        {
            return SideShot;
        }
        set
        {
            SideShot = value;
        }
    }
    public bool ShotBack
    {
        get
        {
            return BackShot;
        }
        set
        {
            BackShot = value;
        }
    }
    public bool ShotPiece
    {
        get
        {
            return PieceShot;
        }
        set
        {
            PieceShot = value;
        }
    }
    public bool PCisDead
    {
        get
        {
            return _isDead;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _UniqueInstance = this;
        InitStatData("Archer", 10, 10);
        Ring = Instantiate(TargetRing, GameObject.FindGameObjectWithTag("InGameObject").transform);
        Ring.SetActive(false);
        JS = GameObject.FindGameObjectWithTag("JS").GetComponent<JSController>();
        animator = GetComponent<Animator>();
        _maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Start()
    {
        HPui = Instantiate(HPbar, GameObject.FindGameObjectWithTag("WorldCanvas").transform).transform.GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        SetUIInfo();
        if ( _CurHP ==0)
        {
            _isDead = true;
            _currentState = eMotion.DIE;
        }
        if (InGameManager._instance.CheckCurMonster())
        {
            Targets = GameObject.FindGameObjectsWithTag("Monster");
        }
        switch (_currentState)
        {
            case eMotion.MOVE:
                Ring.SetActive(false);
                ChangeAni(eMotion.MOVE);
                return;
            case eMotion.IDLE:
                ChangeAni(eMotion.IDLE);
                return;
            case eMotion.HIT:
                ChangeAni(eMotion.HIT);
                _time += Time.deltaTime;
                if (_time >= 0.767)
                    _currentState = eMotion.IDLE;
                return;
            case eMotion.ATTACK:
                DetectingTarget();
                Ring.SetActive(true);
                ChangeAni(eMotion.ATTACK);
                //AttackArrow();
                return;
            case eMotion.DIE:
                animator.SetTrigger("DIE");
                transform.position = transform.position;
                _maincamera.orthographicSize = 4.0f;
                return;
        }
    }
    public void ChangeAni(eMotion ani)
    {
        animator.SetInteger("AniState", (int)ani);
    }
    public void InitStatData(string name, int addAtt, int addLife)
    {
        _Name = name;
        InitData(10, 50);
        AddData(addAtt, addLife);
    }
    
    public void SetUIInfo()
    {
        Vector3 screenPos = _maincamera.WorldToScreenPoint(transform.position);
        HPui.transform.position = new Vector3(screenPos.x, screenPos.y+40, HPui.transform.position.z);
        HPui.maxValue = _MaxHP;
        HPui.value = _CurHP;
        
    }
    public bool HitDamege(int damage)
    {
        _currentState = eMotion.HIT;
        return DamageMe(damage);
    }
    public void AttackArrow()
    {
        if(Target == null)
        {
            return;
        }
        else
        {
            transform.LookAt(Target.transform.position);
            GameObject go = Instantiate(Weapon, WeaponPos.transform.position, transform.rotation);
            SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.SHOOT);
            go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            if (SideShot)
            {
                go = Instantiate(Weapon, Side1.transform.position, Side1.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
                go = Instantiate(Weapon, Side2.transform.position, Side2.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            }
            if (BackShot)
            {
                go = Instantiate(Weapon, Back.transform.position, Back.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            }
        }
    }
    public void AttackArrow2()
    {
        if(ArrowCount>=2)
        {
            GameObject go = Instantiate(Weapon, WeaponPos.transform.position, transform.rotation);
            SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.SHOOT);
            go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            if (SideShot)
            {
                go = Instantiate(Weapon, Side1.transform.position, Side1.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
                go = Instantiate(Weapon, Side2.transform.position, Side2.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            }
            if (BackShot)
            {
                go = Instantiate(Weapon, Back.transform.position, Back.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            }
        }
    }
    public void AttackArrow3()
    {
        if(ArrowCount>=3)
        {
            GameObject go = Instantiate(Weapon, WeaponPos.transform.position, transform.rotation);
            SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.SHOOT);
            go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            if (SideShot)
            {
                go = Instantiate(Weapon, Side1.transform.position, Side1.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
                go = Instantiate(Weapon, Side2.transform.position, Side2.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            }
            if (BackShot)
            {
                go = Instantiate(Weapon, Back.transform.position, Back.transform.rotation);
                go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
            }
        }
    }
    public void DetectingTarget()
    {
        
        Target = Targets[0];
        for (int i = 1; i< Targets.Length; i++)
        {
            if (Target == null || Targets[0] == null)
            {
                return;
            }
            if (Vector3.Distance(Target.transform.position,transform.position) >Vector3.Distance(Targets[i].transform.position,transform.position))
            {
                Target = Targets[i];
                Ring.SetActive(true);
            }
        }
        Ring.transform.position = Target.transform.position;
    }
    public bool IsThereMonster()
    {
        if (Targets == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void CreatResultWnd()
    {
        Time.timeScale = 0.0f;
        GameObject go = Instantiate(Resultwnd,GameObject.FindGameObjectWithTag("UIFrame").transform);
        SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.DIE);
    }
    
}
