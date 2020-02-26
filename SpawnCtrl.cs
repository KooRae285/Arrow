using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabMonster;
    float _timeSpawn = 5;
    int _limitCountSpawn;
    float _timeCheck;
    List<GameObject> monsters;
    Vector3 SpawnPosition;
    PlayerController pc;
    int KindofMonster = 0;
    // Start is called before the first frame update
    void Awake()
    {
        monsters = new List<GameObject>();
    }
    public int _curLiveMonsterCount
    {
        get
        {
            return monsters.Count;
        }
    }
    public List<GameObject> _curMonster
    {
        get
        {
            return monsters;
        }
    }


    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InGameManager._instance._EnableSpawn)
        {
            if (monsters.Count < _limitCountSpawn)
            {
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= _timeSpawn)
                {
                    _timeCheck = 0;
                    SpawnMonster();
                    InGameManager._instance.CheckCountMonster();

                }
            }
        }
        switch(InGameManager._instance.currentPhase)
        {
            case InGameManager.eGamePhase.Phase1:
                KindofMonster = 2;
                _limitCountSpawn = 5;
                break;
            case InGameManager.eGamePhase.Phase2:
                KindofMonster = 3;
                _limitCountSpawn = 10;
                break;
            case InGameManager.eGamePhase.Phase3:
                KindofMonster = 4;
                _limitCountSpawn = 15;
                break;
            case InGameManager.eGamePhase.Phase4:
                _limitCountSpawn = 20;
                break;
        }
    }
    void LateUpdate()
    {
        foreach (GameObject i in monsters)
        {
            if (i == null)
            {
                monsters.Remove(i);
                InGameManager._instance.CheckCountMonster();
                break;
            }
        }

    }
    void SpawnMonster()
    {
        int i = Random.Range(0, KindofMonster);
        float j = Random.Range(-2, 2);
        float k = Random.Range(-2, 2);
        SpawnPosition = new Vector3(j, 0, k);
        
        GameObject go = Instantiate(_prefabMonster[i], transform.position+SpawnPosition, transform.rotation);
        go.transform.SetParent(GameObject.FindGameObjectWithTag("InGameObject").transform);
        monsters.Add(go);
       
    }
}
