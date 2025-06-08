using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDialouge : MonoBehaviour
{
    public static PlayerDialouge Instance { get; private set;}
    [SerializeField][Min(1f)] float textDuration = 3f;
    TextMeshProUGUI tmp;
    CanvasGroup cg;
    bool dialougeIsPlaying = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this; 

        tmp = GetComponentInChildren<TextMeshProUGUI>();
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
    }

    public void PlayDialouge(string dialouge)
    {
        tmp.text = dialouge;
        if (!dialougeIsPlaying) StartCoroutine(ProcessDialouge());
    }

    IEnumerator ProcessDialouge()
    {
        dialougeIsPlaying = true;
        cg.blocksRaycasts = true;
        float timer = 0f;
        float duration = 0.5f;
        while (timer < duration)
        {
            cg.alpha = timer / duration;
            yield return null;
            timer += Time.deltaTime;
        }
        cg.alpha = 1f;
        timer = 0f;
        yield return new WaitForSeconds(textDuration - 2 * duration);
        while (timer < duration)
        {
            cg.alpha = 1 - (timer / duration);
            yield return null;
            timer += Time.deltaTime;
        }
        cg.alpha = 0f;
        dialougeIsPlaying = false;
        tmp.text = "";
    }
}
