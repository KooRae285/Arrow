using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum eBGMType
    {
        LOOBY = 0,
        PLAY1
    }

    public enum eEffectType
    {
        BUTTON1 = 0,
        CLICK1,
        MUMMY,
        STONE,
        HIT,
        SHOOT,
        DAMAGE,
        DIE,
    }

    [SerializeField] GameObject _soundmanager;
    [SerializeField] AudioClip[] _bgmClips;
    [SerializeField] AudioClip[] _effClips;

    AudioSource _bgmPlayer;
    AudioSource _effPlayer;
    float _BgVol = 1.0f;
    bool _BgCheck = true;
    float _EfVol = 1.0f;
    bool _EfCheck = true;
    List<AudioSource> _ltEffPlayer;
    public float BGVOL
    {
        get
        {
            return _BgVol;
        }
        set
        {
            _BgVol = value;
        }
    }
    public bool CheckBGVol
    {
        get
        {
            return _BgCheck; 
        }
        set
        {
            _BgCheck = value;
        }
    }
    public float EFVOL
    {
        get
        {
            return _EfVol;
        }
        set
        {
            _EfVol = value;
        }
    }
    public bool CheckEFVol
    {
        get
        {
            return _EfCheck;
        }
        set
        {
            _EfCheck = value;
        }
    }
    static SoundManager _uniqueInstnace;

    public static SoundManager _instance
    {
        get
        {
            return _uniqueInstnace;
        }
    }

    void Awake()
    {
        _uniqueInstnace = this;

        _bgmPlayer = GetComponent<AudioSource>();

        _ltEffPlayer = new List<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (!_BgCheck)
        {
            _bgmPlayer.volume = 0;
        }
        else
        {
            _bgmPlayer.volume = _BgVol;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        foreach (AudioSource item in _ltEffPlayer)
        {
            if (!item.isPlaying)
            {
                _ltEffPlayer.Remove(item);
                Destroy(item.gameObject);
                break;
            }
        }
    }
    public void PlayBGMSound(eBGMType type, float vol = 1.0f, bool isloop = true)
    {
        _bgmPlayer.clip = _bgmClips[(int)type];
        _bgmPlayer.volume = _BgVol;
        _bgmPlayer.loop = isloop;

        _bgmPlayer.Play();
    }
    public void PlayEffectSound(eEffectType type, float vol = 1.0f, bool isloop = false)
    {
        GameObject go = Instantiate(_soundmanager);
        go.transform.SetParent(transform);
        _effPlayer = go.GetComponent<AudioSource>();

        _effPlayer.clip = _effClips[(int)type];
        _effPlayer.volume = _EfVol;
        _effPlayer.loop = isloop;

        _effPlayer.Play();
        _ltEffPlayer.Add(_effPlayer);


    }
}
