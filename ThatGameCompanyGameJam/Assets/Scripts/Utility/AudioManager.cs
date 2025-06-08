using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField] EventReference musicReference;
    EventInstance musicInstance;

    private void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicReference);
        musicInstance.start();
    }
}
