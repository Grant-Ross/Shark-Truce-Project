using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio; 
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance ? _instance : _instance = FindObjectOfType<AudioManager>();

    //private static Sound[] Sounds;

    private static Dictionary<string, AudioSource> SoundSources = new Dictionary<string, AudioSource>();
    
    
    private static Music[] Music;

    [SerializeField] private AudioSource musicSource;
    private Music _currentMusic;

    private bool _musicSwitch = false;
    
    private void Awake()
    {
        //CreateSounds();
        //CreateMusic();
    }

    private void CreateSounds()
    {
        var sounds = Resources.LoadAll<Sound>("Sound Objects");
        SoundSources = new Dictionary<string, AudioSource>();
        foreach (var s in sounds)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            SoundSources.Add(s.soundName, source);
        }
    }

    private void CreateMusic()
    {
        Music = Resources.LoadAll<Music>("Music");
        //musicSource = gameObject.AddComponent<AudioSource>();
    }

    public static void PlaySound(string sound)
    {
        return;
        if(SoundSources.ContainsKey(sound)) SoundSources[sound].Play();
    }

    public void PlayMusic(string music)
    {
        return;
        foreach (var m in Music)
        {
            if (m.soundName == music)
            {
                if (_currentMusic != null && _currentMusic.soundName == m.soundName) return;
                _musicSwitch = true;
                StartCoroutine(MusicRoutine(m));
                return;
            }
        }
    }

    public Music GetMusic(string music)
    {
        return new Music();
        foreach (var m in Music)
        {
            if (m.soundName == music) return m;
        }
        throw new MissingReferenceException();
    }

    private IEnumerator MusicRoutine(Music music)
    {
        yield return null;
        _musicSwitch = false;
        _currentMusic = music;
        musicSource.clip = music.head;
        musicSource.loop = false;
        musicSource.Play();
        while (musicSource.isPlaying && !_musicSwitch) yield return null;
        musicSource.clip = music.clip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
 