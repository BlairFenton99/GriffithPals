using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource[] audioSources;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<SoundManager>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

        Load();

        // Update slider in new scenes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find new slider if we changed scenes
        volumeSlider = FindObjectOfType<Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        }

        // Find audio sources in new scene
        audioSources = FindObjectsOfType<AudioSource>();
        ApplyCurrentVolume();
    }

    public void ChangeVolume() //Handles changing volume
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        ApplyCurrentVolume();
        PlayerPrefs.Save();
    }

    void Load() //saves state from last scene
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        ApplyCurrentVolume();
    }

    void ApplyCurrentVolume() //applies volume
    {
        float volume = PlayerPrefs.GetFloat("musicVolume");
        foreach (var source in audioSources)
        {
            if (source != null) source.volume = volume;
        }
    }
}