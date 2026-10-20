using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private SoundGroup paddleHit;
    [SerializeField] private SoundGroup brickBreak;
    [SerializeField] private SoundGroup loseLife;
    [SerializeField] private SoundGroup wallBounce;

    private void Awake()
    {
        Instance = this;

        SetupSource(paddleHit);
        SetupSource(brickBreak);
        SetupSource(loseLife);
        SetupSource(wallBounce);
    }

    private void SetupSource(SoundGroup group)
    {
        group.source = gameObject.AddComponent<AudioSource>();
        group.source.playOnAwake = false;
    }

    private void Play(SoundGroup group)
    {
        if (group.clips.Length == 0) return;

        AudioClip clip = group.clips[UnityEngine.Random.Range(0, group.clips.Length)];
        group.source.pitch = UnityEngine.Random.Range(group.minPitch, group.maxPitch);
        group.source.PlayOneShot(clip, group.volume);   
    }

    public void PlayPaddleHit() => Play(paddleHit);
    public void PlayBrickBreak() => Play(brickBreak);
    public void PlayLoseLife() => Play(loseLife);
    public void PlayWallBounce() => Play(wallBounce);
}

[System.Serializable]
public class SoundGroup
{
    public AudioClip[] clips;
    [Range(0f, 1f)] public float volume = 1f;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    [HideInInspector] public AudioSource source;
}