using UnityEngine;

[System.Serializable]
public class SFX
{
    public string groupID;
    public AudioClip[] clips;
}

public class SFXLib : MonoBehaviour
{
    public SFX[] sfxs;
    public AudioClip GetClipByName(string name)
    {
        foreach (SFX sfx in sfxs)
        {
            if (sfx.groupID == name)
            {
                return sfx.clips[Random.Range(0, sfx.clips.Length)]; // Return a random clip in the group
            }
        }
        Debug.LogWarning("SFX group not found: " + name);
        return null;
    }
}
