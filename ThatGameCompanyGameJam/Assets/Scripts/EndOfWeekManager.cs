using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class EndOfWeekManager : MonoBehaviour
{
    [SerializeField] PlayableAsset intro;
    [SerializeField] PlayableAsset goodEnding;
    [SerializeField] PlayableAsset badEnding;
    [SerializeField] PlayableAsset neutralEnding;
    [SerializeField] PlayableAsset corpoEnding;

    PlayableDirector pd;
    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        StartCoroutine(DelayForIntro());
    }

    private IEnumerator DelayForIntro()
    {
        yield return new WaitForSeconds((float)intro.duration);
        ProcessEnd();
    }

    private void ProcessEnd()
    {
        if (PlayerDataManager.Instance == null)
        {
            pd.Play(neutralEnding);
            return;
        }

        EndingTypes endType = PlayerDataManager.Instance.GetEndingType();
        switch (endType)
        {
            case EndingTypes.Bad:
                pd.Play(badEnding);
                break;
            case EndingTypes.Corpo:
                pd.Play(corpoEnding);
                break;
            case EndingTypes.Good:
                pd.Play(goodEnding);
                break;
            case EndingTypes.Neutral:
                pd.Play(neutralEnding);
                break;
            default:
                pd.Play(neutralEnding);
                break;
        }
    }
}
