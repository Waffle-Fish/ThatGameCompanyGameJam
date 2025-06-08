using UnityEngine;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text musicVolumeTextUI;
    [SerializeField] private TMP_Text sfxVolumeTextUI;

    public void UpdateMusicVolume(float value)
    {
        musicVolumeTextUI.text = Mathf.Round(value * 100).ToString() + "%";

        if (AudioManager.Main != null)
            AudioManager.Main.SetMusicVolume(value);
    }

    public void UpdateSFXVolume(float value)
    {
        sfxVolumeTextUI.text = Mathf.Round(value * 100).ToString() + "%";

        if (AudioManager.Main != null)
            AudioManager.Main.SetSFXVolume(value);
    }
}
