using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public enum eGameState
    {

    }
    public enum eGamePhase
    {
        Phase1 =0,
        Phase2,
        Phase3,
        Phase4,
    }
    [SerializeField] GameObject abilchoice;
    [SerializeField] GameObject SpawnPlayer;
    [SerializeField] GameObject _prebPlayer;
    [SerializeField] GameObject Bee;
    [SerializeField] GameObject IGMenuWnd;
    [SerializeField] Text ScoreTxt;
    [SerializeField] Text MnTime;
    [SerializeField] Text ScTime;
    int _maxMonsterCount = 5;
    eGameState curState;
    eGamePhase curPhase;
    SpawnCtrl[] _ctrlSpawn;
    bool _isSpawn = true;
    int _killPoint = 0;
    int ChoiceCount = 5;
    bool _openUI = false;
    bool UItrigger = false;
    bool Hphh = false;
    int Score = 0;
    int TotalScore = 0;
    float TimeSc = 0;
    int TimeMn = 0;
    List<Sprite> HadAbil;

    // Start is called before the first frame update
    static InGameManager _uniqueinstance;
    public static InGameManager _instance
    {
        get
        {
            return _uniqueinstance;
        }
    }
    public bool _EnableSpawn
    {
        get
        {
            return _isSpawn;
        }
    }
    public bool _isHphh
    {
        get
        {
            return Hphh;
        }
        set
        {
            Hphh = value;
        }
    }
    public bool _UIopen
    {
        get
        {
            return _openUI;
        }
        set
        {
            _openUI = value;
        }
    }
    public int Killcount
    {
        get
        {
            return _killPoint;
        }
        set
        {
            _killPoint = value;
        }
    }
    public eGamePhase currentPhase
    {
        get
        {
            return curPhase;
        }
    }
    public int ResultScore
    {
        get
        {
            return TotalScore;
        }
    }
    public float Sctime
    {
        get
        {
            return TimeSc;
        }
    }
    public float Mntime
    {
        get
        {
            return TimeMn;
        }
    }
    
    private void Awake()
    {
        HadAbil = new List<Sprite>();
        _uniqueinstance = this;
        SpawnChar();//임시
        _ctrlSpawn = FindObjectsOfType<SpawnCtrl>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Score != 0)
        {
                TotalScore++;
                ScoreTxt.text = TotalScore.ToString();
                Score--;   
        }
        TimeReset();
        if (_killPoint < 10)
        {
            curPhase = eGamePhase.Phase1;
        }
        if (_killPoint < 20 && _killPoint >= 10)
        {
            curPhase = eGamePhase.Phase2;
        }
        if (_killPoint < 30 && _killPoint >= 20)
        {
            curPhase = eGamePhase.Phase3;
        }
        if (_killPoint >= 30)
        {
            curPhase = eGamePhase.Phase4;
        }
        //switch()
        if (_killPoint ==ChoiceCount)
        {
            Debug.Log("choiceeeeeeeeeee");
            if (!UItrigger)
            {
                Time.timeScale = 0.0f;
                ChoiceCount += _killPoint;
                _openUI = true;
                UItrigger = true;
                abilchoice.SetActive(_openUI);
            }
        }
        else
        {
            UItrigger = false;
            abilchoice.SetActive(_openUI);
        }
    }

    void SpawnChar()
    {
        GameObject go = Instantiate(_prebPlayer, GameObject.FindGameObjectWithTag("InGameObject").transform);
        go.transform.position = SpawnPlayer.transform.position;
    }
    public void CheckCountMonster()
    {
        int tCount = 0;
        for (int n = 0; n < _ctrlSpawn.Length; n++)
        {
            tCount += _ctrlSpawn[n]._curLiveMonsterCount;
        }

        if (tCount >= _maxMonsterCount)
        {
            _isSpawn = false;
        }
        else
            _isSpawn = true;
    }
    public bool CheckCurMonster()
    {
        int tCount = 0;
        for (int n = 0; n < _ctrlSpawn.Length; n++)
        {
            tCount += _ctrlSpawn[n]._curLiveMonsterCount;
        }
        if(tCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void ClickIGMenu()
    {
        Time.timeScale = 0.0f;
        IGMenuWnd.SetActive(true);
    }
    public void InGameScore(int MonsterScore)
    {
        Score += MonsterScore;
    }
    public void TimeReset()
    {
        TimeSc += Time.deltaTime;
        if (TimeSc >= 60)
        {
            TimeSc = 0;
            TimeMn++;
        }
        ScTime.text = string.Format("{0:N1}", TimeSc);
        MnTime.text = TimeMn.ToString();
    }
    public void AddAbilImage(Sprite image)
    {
        HadAbil.Add(image);
    }
    
}
