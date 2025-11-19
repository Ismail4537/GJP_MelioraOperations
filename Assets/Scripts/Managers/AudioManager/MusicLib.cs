using UnityEngine;

[System.Serializable]
public class MusicTrack
{
    public string trackName;
    public AudioClip clip;
}

public class MusicLib : MonoBehaviour
{
    public MusicTrack[] musicTracks;
    public AudioClip GetTrackByName(string name)
    {
        foreach (MusicTrack track in musicTracks)
        {
            if (track.trackName == name)
            {
                return track.clip;
            }
        }
        Debug.LogWarning("Music track not found: " + name);
        return null;
    }
}
