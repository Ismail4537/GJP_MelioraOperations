using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private MusicLib musicLib;
    [SerializeField] private AudioSource musicObject;
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

    public void PlayMusicTrack(string name, float fadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusicCrossfade(musicLib.GetTrackByName(name), fadeDuration));
    }

    public void playTest1()
    {
        PlayMusicTrack("Test1", 1f);
    }

    public void playTest2()
    {
        PlayMusicTrack("Test2", 1f);
    }

    public void StopMusicTrack(float fadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusicCrossfade(null, fadeDuration));
    }

    IEnumerator AnimateMusicCrossfade(AudioClip newClip, float fadeDuration = 0.5f)
    {
        float percent = 0f;
        while (percent < 1f)
        {
            percent += Time.deltaTime / fadeDuration;
            musicObject.volume = Mathf.Lerp(1f, 0f, percent);
            yield return null;
        }

        musicObject.clip = newClip;
        if (musicObject.clip != null)
        {
            musicObject.Play();

            percent = 0f;
            while (percent < 1f)
            {
                percent += Time.deltaTime / fadeDuration;
                musicObject.volume = Mathf.Lerp(0f, 1f, percent);
                yield return null;
            }
        }
    }
}