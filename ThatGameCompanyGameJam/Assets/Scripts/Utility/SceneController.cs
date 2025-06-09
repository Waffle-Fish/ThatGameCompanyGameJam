using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ReloadScene()
    {
        AudioManager.Main.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        AudioManager.Main.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int sceneIndex)
    {
        AudioManager.Main.StopMusic();
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadScene(int sceneIndex, float delay)
    {
        IEnumerator DelayLoadScene()
        {
            yield return new WaitForSeconds(delay);
            LoadScene(sceneIndex);
        }

        StartCoroutine(DelayLoadScene());
    }

    // Load Scene after cutscene plays
    public void LoadScene(int sceneIndex, PlayableAsset cutscene)
    {
        LoadScene(sceneIndex, (float)cutscene.duration);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
