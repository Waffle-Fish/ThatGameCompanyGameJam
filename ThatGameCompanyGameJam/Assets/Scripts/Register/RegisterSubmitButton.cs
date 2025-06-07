using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RegisterSubmitButton : CustomButton
{
    Animator animator;
    RegisterManager registerManager;
    Button button;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        registerManager = GetComponentInParent<RegisterManager>();
        button = GetComponent<Button>();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        IEnumerator DelayClose()
        {
            button.interactable = false;
            yield return new WaitForSeconds(0.25f);
            button.interactable = true;
            registerManager.gameObject.SetActive(false);
        }
        animator.SetBool("Pressed", false);
        registerManager.SubmitTotal();
        // play sfx
        StartCoroutine(DelayClose());
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        animator.SetBool("Pressed", true);
        // play sfx
    }
}
