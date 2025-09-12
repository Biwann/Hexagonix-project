using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Music Data")]
public class MusicData : ScriptableObject
{
    public AudioClip Clip;

    [Range(0f, 1f)] public float Volume = 0.7f;
    public bool ShouldLoop = true;

    [Header("Fade Settings")]
    public float FadeInDuration = 2f;
    public float FadeOutDuration = 2f;
}