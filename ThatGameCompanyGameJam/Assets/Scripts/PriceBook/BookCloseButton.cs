using UnityEngine;
using UnityEngine.EventSystems;

public class BookCloseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse has entered " + name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse has exited " + name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.gameObject.SetActive(false);
    }
}
