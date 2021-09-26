using UnityEngine.Audio; 
using UnityEngine;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public string name; 

    public AudioClip clip;

    [Range (0f, 1f)]
    public float volume = 1;
    [Range(.1f, 3f)]
    public float pitch = 0;

    [HideInInspector]
    public AudioSource source; 

}