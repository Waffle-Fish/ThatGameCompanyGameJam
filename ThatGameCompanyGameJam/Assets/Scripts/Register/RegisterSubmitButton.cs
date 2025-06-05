using UnityEngine;
using UnityEngine.EventSystems;

public class RegisterSubmitButton : CustomButton
{
    [SerializeField][Range(0f, 1f)] float hoverAlpha = 0.9f;
    Animator animator;
    RegisterManager registerManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        registerManager = GetComponentInParent<RegisterManager>();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        animator.SetBool("Pressed", false);
        registerManager.SubmitTotal();
        // play sfx
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        animator.SetBool("Pressed", true);
        // play sfx
    }
}
