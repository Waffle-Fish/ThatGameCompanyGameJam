using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Main;

    [SerializeField] EventReference musicReference;
    private EventInstance musicInstance;

    private VCA musicVCA;
    private VCA sfxVCA;

    private void Awake()
    {
        if (Main == null)
            Main = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        musicVCA = RuntimeManager.GetVCA("vca:/Music");
        sfxVCA = RuntimeManager.GetVCA("vca:/SFX");

        musicInstance = RuntimeManager.CreateInstance(musicReference);
        musicInstance.start();
    }

    public void SetMusicVolume(float volume)
    {
        musicVCA.setVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVCA.setVolume(volume);
    }
}
