using UnityEngine.Audio; 
using UnityEngine;

public class AudioManager : MonoBehaviour 
{

    public Sound[] sounds;

    // use this for initialization
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>()
AudioSource source = s.source;
            source.clip = s.clip;


            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

    }
         

}
 