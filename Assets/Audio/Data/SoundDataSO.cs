using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Sound Data")]
public class SoundData : ScriptableObject
{
    public AudioClip Clip;

    [Range(0f, 1f)] public float Volume = 1f;
    [Range(0.1f, 3f)] public float Pitch = 1f;
    public bool ShoudLoop = false;
    public bool IsRandomPitch = false;
    [Range(0f, 1f)] public float RandomPitchRange = 0.1f;
}