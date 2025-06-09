using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] EventReference clickReference;

    EventInstance clickInstance;

    protected Image buttonImg;

    private void Start()
    {
        buttonImg = GetComponent<Image>();
        // buttonImg.alphaHitTestMinimumThreshold = 0.5f;

        clickInstance = RuntimeManager.CreateInstance(clickReference);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        PlayClickSFX();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

    protected void PlayClickSFX()
    {
        clickInstance.start();
    }
}
