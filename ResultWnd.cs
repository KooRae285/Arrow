using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWnd : MonoBehaviour
{
    [SerializeField] Text TimeSC;
    [SerializeField] Text TimeMN;
    [SerializeField] Text KillScore;
    [SerializeField] Text TotalScore;
    [SerializeField] Text Rank;

    int Kill = 0;
    int SC = 0;
    float MN = 0;
    float Total = 0;
    bool EventTriger = false;
    bool Complete = false;
    string FinalRank;
    void Awake()
    {
        Kill= InGameManager._instance.ResultScore;
        SC = (int)(InGameManager._instance.Sctime);
        MN = InGameManager._instance.Mntime;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RankFinal();
        KillScore.text = Kill.ToString();
        TimeSC.text = SC.ToString();
        TimeMN.text = MN.ToString();
        TotalScore.text = Total.ToString();
        if (EventTriger)
        {
            if(Kill !=0)
            {
                Kill--;
                Total += 10;
            }
            if( !(SC ==0 && MN==0))
            {
                if (SC == 0)
                {
                    MN--;
                    SC += 60;
                }
                SC--;
                Total += 1;
            }
        }
        if (Kill == 0 && MN ==0 && SC ==0)
        {
            if (!Complete)
            {
                Text go = Instantiate(Rank,transform);
                go.text = FinalRank;
                Complete = true;
            }
        }
        
    }
    public void OnComplete()
    {
        EventTriger = true;
    }
    public void RankFinal()
    {
        if(Total < 10000)
        {
            FinalRank = ("F");
        }
        if(Total >= 10000)
        {
            FinalRank = ("B");
        }
        if (Total >= 50000)
        {
            FinalRank = ("A");
        }
        if (Total >= 100000)
        {
            FinalRank = ("S");
        }
        if (Total >= 20000)
        {
            FinalRank = ("SS");
        }
    }
}
