using UnityEngine;
using UnityEngine.EventSystems;

public class PriceBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    GameObject bookPopUp;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bookPopUp = transform.GetChild(0).gameObject;
        bookPopUp.SetActive(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
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
        if (!bookPopUp.activeSelf)
        {
            spriteRenderer.enabled = false;
            bookPopUp.SetActive(true);
        }
    }
}
