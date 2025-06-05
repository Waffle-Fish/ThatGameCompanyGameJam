using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] AudioClip hoverSFX;
    [SerializeField] AudioClip clickSFX;
    
    protected Image buttonImg;

    private void Start()
    {
        buttonImg = GetComponent<Image>();
        // buttonImg.alphaHitTestMinimumThreshold = 0.5f;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
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

    protected void PlayHoverSFX()
    {
        // throw new System.NotImplementedException();
    }

    protected void PlayClickSFX()
    {
        // throw new System.NotImplementedException();
    }
}
