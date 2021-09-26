using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio; 
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance ? _instance : _instance = FindObjectOfType<AudioManager>();

    private static Sound[] Sounds;

    private static Dictionary<string, AudioSource> SoundSources = new Dictionary<string, AudioSource>();
    
    
    private static Music[] Music;

    [SerializeField] private AudioSource musicSource;
    private Music _currentMusic;

    private bool _musicSwitch = false;
    
    private void Awake()
    {
        CreateSounds();
        CreateMusic();
    }

    private void CreateSounds()
    {
        Sounds = Resources.LoadAll<Sound>("Sound Objects");
        
        foreach (var s in Sounds)
        {
            var source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            SoundSources.Add(s.soundName, source);
        }
    }

    private void CreateMusic()
    {
        Music = Resources.LoadAll<Music>("Sound Objects/Music");
        //musicSource = gameObject.AddComponent<AudioSource>();
    }

    public static void PlaySound(string sound)
    {
        SoundSources[sound].Play();
    }

    public void PlayMusic(string music)
    {
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
 