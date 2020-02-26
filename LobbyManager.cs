using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject OptionWnd;
    [SerializeField] Slider BGvolGage;
    [SerializeField] Slider EFvolGage;
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SoundControl();
    }
    public void ClickStartButton()
    {
        BaseManager._instance.StartPlayScene("LobbyScene");
        SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.CLICK1);
    }
    public void ClickShopButton()
    {
        OptionWnd.SetActive(true);
        SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.CLICK1);
    }
    public void ClickExitButton()
    {
        SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.CLICK1);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ClickCloseButton()
    {
        OptionWnd.SetActive(false);
    }
    public void OnPointEnter()
    {
        SoundManager._instance.PlayEffectSound(SoundManager.eEffectType.BUTTON1);
    }
    public void SoundControl()
    {
        SoundManager._instance.BGVOL = BGvolGage.value;
        SoundManager._instance.EFVOL = EFvolGage.value;
    }
    public void BgCheck()
    {
        SoundManager._instance.CheckBGVol = !SoundManager._instance.CheckBGVol;
    }
    public void EfCheck()
    {
        SoundManager._instance.CheckEFVol = !SoundManager._instance.CheckEFVol;
    }

}
