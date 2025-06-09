using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] EventReference hoverReference;
    [SerializeField] EventReference clickReference;

    EventInstance hoverInstance;
    EventInstance clickInstance;

    protected Image buttonImg;

    private void Start()
    {
        buttonImg = GetComponent<Image>();
        // buttonImg.alphaHitTestMinimumThreshold = 0.5f;

        if (hoverReference.Path.Length > 0)
            hoverInstance = RuntimeManager.CreateInstance(hoverReference);
        if (clickReference.Path.Length > 0)
            clickInstance = RuntimeManager.CreateInstance(clickReference);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        PlayClickSFX();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSFX();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

    protected void PlayHoverSFX()
    {
        if(hoverReference.Path.Length > 0)
            hoverInstance.start();
    }

    protected void PlayClickSFX()
    {
        if (clickReference.Path.Length > 0)
            clickInstance.start();
    }
}
