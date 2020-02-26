using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BaseManager : MonoBehaviour
{
    [SerializeField] GameObject LoadingWnd;

    public enum eSceneState
    {
        Lobby = 0,
        Loading,
        Play,
        Shop
    }
    

    eSceneState _currentScene;
    static BaseManager _uniqueinstance;
    public static BaseManager _instance
    {
        get
        {
            return _uniqueinstance;
        }
    }
    private void Awake()
    {
        _uniqueinstance = this;
        Screen.SetResolution(1080, 1920, true);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartLobbyScene(string unloadScene = "")
    {
        _currentScene = eSceneState.Lobby;
        StartCoroutine(LoadingScene(unloadScene, "LobbyScene"));
        SoundManager._instance.PlayBGMSound(SoundManager.eBGMType.LOOBY);
    }
    public void StartPlayScene(string unloadScene = "")
    {
        _currentScene = eSceneState.Play;
        StartCoroutine(LoadingScene(unloadScene, "InGameScene"));
        SoundManager._instance.PlayBGMSound(SoundManager.eBGMType.PLAY1);
    }
    IEnumerator LoadingScene(string UnloadName, string LoadName)
    {
        GameObject go = Instantiate(LoadingWnd);
        LoadingManager Loadingwnd = go.GetComponent<LoadingManager>();

        AsyncOperation AO;
        if (UnloadName != string.Empty)
        {
            AO = SceneManager.UnloadSceneAsync(UnloadName);
            while (!AO.isDone)
            {
                Loadingwnd.SettingLoadingBar(0.33f);
                yield return new WaitForSeconds(2);
                yield return null;
            }
        }
        Loadingwnd.SettingLoadingBar(0.33f);
        AO = SceneManager.LoadSceneAsync(LoadName, LoadSceneMode.Additive);
        while (!AO.isDone)
        {
            Loadingwnd.SettingLoadingBar(0.66f);
            yield return new WaitForSeconds(2);
            yield return null;
            
        }
        Loadingwnd.SettingLoadingBar(0.66f);
        while (!AO.isDone)
        {
            Loadingwnd.SettingLoadingBar(1);
            yield return new WaitForSeconds(2);
            yield return null;
        }
        Loadingwnd.SettingLoadingBar(1);

        
        //else
        //{
        //    AO = SceneManager.UnloadSceneAsync("InGameScene");

        //    while (!AO.isDone)
        //    {
        //        Loadingwnd.SettingLoadingBar(0.66f);
        //        yield return null;
        //    }
        //    Loadingwnd.SettingLoadingBar(0.66f);
        //    AO = SceneManager.LoadSceneAsync(LoadName, LoadSceneMode.Additive);
        //    while (!AO.isDone)
        //    {
        //        Loadingwnd.SettingLoadingBar(1);
        //        yield return null;
        //    }
        //    Loadingwnd.SettingLoadingBar(1);
        //    yield return new WaitForSeconds(2);

        //}
        Destroy(Loadingwnd.gameObject);
    }
    IEnumerator GameStart()
    {
        GameObject go = Instantiate(LoadingWnd);
        LoadingManager Loadingwnd = go.GetComponent<LoadingManager>();
        AsyncOperation AO;
        AO = SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive);
        while (!AO.isDone)
        {
            Loadingwnd.SettingLoadingBar(AO.progress);
            yield return null;
        }
        Loadingwnd.SettingLoadingBar(1);
        yield return new WaitForSeconds(2);
        Destroy(Loadingwnd.gameObject);
        SoundManager._instance.PlayBGMSound(SoundManager.eBGMType.LOOBY);
    }
    
}
