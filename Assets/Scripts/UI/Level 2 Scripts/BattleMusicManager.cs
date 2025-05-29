using UnityEngine;

public class BattleMusicManager : MonoBehaviour
{
    public AudioSource musicSource;

    void Start()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}

