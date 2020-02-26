using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuWnd : MonoBehaviour
{
    [SerializeField] List<Sprite> AbilitySprite;
    [SerializeField] GameObject StartPos;
    [SerializeField] Image HadAbil;
    List<Image> hadAbility;
    int row = 0;
    int col = 0;

    public static InGameMenuWnd _UniqueInstance;
    public static InGameMenuWnd _instance
    {
        get
        {
            return _UniqueInstance;
        }
    }
    void Awake()
    {
        _UniqueInstance = this;
        hadAbility = new List<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickReStartButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void ClickExitButton()
    {
        Time.timeScale = 1.0f;
        BaseManager._instance.StartLobbyScene("InGameScene");
    }
    public void AddAbil(Sprite Abimage)
    {
        Image go = Instantiate(HadAbil, transform);
        go.sprite = Abimage;
        go.transform.position = new Vector3(StartPos.transform.position.x + (170 * row), StartPos.transform.position.y + (170 * col), StartPos.transform.position.z);
        row++;
        if(row>=5)
        {
            row = 0;
            col++;
        }
    }
}
