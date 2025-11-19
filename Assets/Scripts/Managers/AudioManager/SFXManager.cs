using System;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private SFXLib sfxLib;
    [SerializeField] private AudioSource SFX3DSource;
    [SerializeField] private AudioSource SFX2DSource;
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayClip3D(AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        if (clip != null)
        {
            AudioSource audioSource = Instantiate(SFX3DSource, position, Quaternion.identity);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.spatialBlend = 1.0f; // 3D sound
            audioSource.Play();
            float clipLength = clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }

    public void PlayClip3D(string name, Vector3 position, float volume = 1.0f)
    {
        PlayClip3D(sfxLib.GetClipByName(name), position, volume);
    }

    public void PlayClip2D(string name, float volume = 1.0f)
    {
        AudioClip clip = sfxLib.GetClipByName(name);
        if (clip != null)
        {
            SFX2DSource.PlayOneShot(clip, volume);
        }
    }
}
