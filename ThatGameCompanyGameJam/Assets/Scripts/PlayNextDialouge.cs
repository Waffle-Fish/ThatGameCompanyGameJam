using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayNextDialouge : MonoBehaviour
{
    PlayableDirector playableDirector;
    TimelineAsset timeline;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        timeline = playableDirector.playableAsset as TimelineAsset;
    }

    void Start()
    {
        TrackAsset dialougeTrack = timeline.GetOutputTrack(7); // Track 7
        var listOfClips = dialougeTrack.GetClips();
        foreach (var item in listOfClips)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
