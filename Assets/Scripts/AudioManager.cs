using System;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType
{
    PaddleHit,
    BrickBreak,
    LoseLife,
    WallBounce,
    PowerUpPickup
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField, Range(0f, 1f)] private float masterVolume = 0.5f;
    [SerializeField] private SoundGroup[] sounds;

    private readonly Dictionary<SoundType, SoundGroup> soundMap = new Dictionary<SoundType, SoundGroup>();


    private void Awake()
    {
        Instance = this;

        foreach (SoundGroup group in sounds)
        {
            group.source = gameObject.AddComponent<AudioSource>();
            group.source.playOnAwake = false;
            soundMap[group.type] = group;
        }
    }

    public void Play(SoundType type)
    {
        if (!soundMap.TryGetValue(type, out SoundGroup group))
        {
            Debug.LogWarning("No hay sonido configurado para: " + type);
            return;
        }

        if (group.clips.Length == 0) return;

        AudioClip clip = group.clips[UnityEngine.Random.Range(0, group.clips.Length)];
        group.source.pitch = UnityEngine.Random.Range(group.minPitch, group.maxPitch);
        group.source.PlayOneShot(clip, group.volume * masterVolume);
    }

}

[System.Serializable]
public class SoundGroup
{
    public SoundType type;
    public AudioClip[] clips;
    [Range(0f, 1f)] public float volume = 1f;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    [HideInInspector] public AudioSource source;
}