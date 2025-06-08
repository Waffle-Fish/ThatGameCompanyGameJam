using UnityEngine;
using UnityEngine.Playables;

public class SkipIntro : MonoBehaviour
{
    [SerializeField] PlayableDirector pd;

    public void SkipTo(int seconds)
    {
        pd.time = seconds;
    }
}
