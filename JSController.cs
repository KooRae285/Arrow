using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JSController : MonoBehaviour
{
    public Transform Stick;
    public Transform Player;
    PlayerController pc;
    private Vector3 StickFirstPos;
    private Vector3 JoyVec;
    private float Radius;
    bool MoveFlag = false;

    static JSController _uniqeinstance;

    public JSController instance
    {
        get
        {
            return _uniqeinstance;
        }
    }

    public bool isMove
    {
        get
        {
            return MoveFlag;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        _uniqeinstance = this;
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        pc = Player.GetComponent<PlayerController>();
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;
        
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pc.PCisDead)
        {
            if (MoveFlag)
            {
                PlayerController._instance.curState = PlayerController.eMotion.MOVE;
                Player.Translate(Vector3.forward * Time.deltaTime * 10);
            }
            else
            {
                if (pc.IsThereMonster())
                    PlayerController._instance.curState = PlayerController.eMotion.ATTACK;
                else
                    PlayerController._instance.curState = PlayerController.eMotion.IDLE;
            }
        }
    }

    public void Drag(BaseEventData _Data)
    {
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        JoyVec = (Pos - StickFirstPos).normalized;

        float Dis = Vector3.Distance(Pos, StickFirstPos);

        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        else
            Stick.position = StickFirstPos + JoyVec * Radius;
        Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
    }
    public void DragEnd()
    {
        Stick.position = StickFirstPos;
        JoyVec = Vector3.zero;
        MoveFlag = false;
    }
}
