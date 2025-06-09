using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class CustomerBehavior : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] String introDialouge;
    [SerializeField] String outroDialouge;
    [SerializeField] String discountDialouge;
    [SerializeField] String overchargeDialouge;

    [Header("Animation Settings")]
    [SerializeField] CustomerScriptableObject customerSO;
    [SerializeField] AnimationClip enterAnimation;
    [SerializeField] AnimationClip exitAnimation;
    [SerializeField] Sprite customerBack;

    [Header("")]

    [Header("FMOD")]
    [SerializeField] EventReference doorReference;
    [SerializeField] EventReference walkingReference;
    [SerializeField] EventReference neutralDialogueReference;
    [SerializeField] EventReference happyDialogueReference;
    [SerializeField] EventReference angryDialogueReference;

    private EventInstance doorInstance;
    private EventInstance walkingInstance;
    private EventInstance neutralDialogueInstance;
    private EventInstance happyDialogueInstance;
    private EventInstance angryDialogueInstance;

    [Header("Components Ref")]
    private SpriteRenderer spriteRenderer;
    private FoodSpawner foodSpawner;
    private TextMeshPro dialogueTMP;
    private GameObject speechBubbleObj;
    private Color originalTextColor;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foodSpawner = FoodSpawner.Instance;
        dialogueTMP = GetComponentInChildren<TextMeshPro>(true);
        animator = GetComponent<Animator>();

        doorInstance = RuntimeManager.CreateInstance(doorReference);
        walkingInstance = RuntimeManager.CreateInstance(walkingReference);
        neutralDialogueInstance = RuntimeManager.CreateInstance(neutralDialogueReference);
        happyDialogueInstance = RuntimeManager.CreateInstance(happyDialogueReference);
        angryDialogueInstance = RuntimeManager.CreateInstance(angryDialogueReference);

        speechBubbleObj = dialogueTMP.transform.parent.gameObject;
        originalTextColor = dialogueTMP.color;
        customerSO.InitializeFoodOrder();
    }

    private void OnEnable()
    {
        StartCoroutine(ProcessEntrance());
    }

    public void DisplayOrder()
    {
        foreach (var order in customerSO.FoodOrder)
        {
            foodSpawner.PlaceFood(order.FoodSO);
        }
        if (introDialouge != "") PlayDialouge(introDialouge);
        else PlayDialouge("I'll have these please");

        neutralDialogueInstance.start();
    }
    public IEnumerator ProcessEntrance()
    {
        doorInstance.start();
        walkingInstance.start();
        animator.SetTrigger("Enter");
        yield return new WaitForSeconds(enterAnimation.length);
        walkingInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        DisplayOrder();
    }

    public IEnumerator ProcessExit()
    {
        spriteRenderer.sprite = customerBack;
        speechBubbleObj.SetActive(false);
        foodSpawner.ReleaseAllActive();
        animator.SetTrigger("Exit");
        walkingInstance.start();
        yield return new WaitForSeconds(exitAnimation.length);
        walkingInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        doorInstance.start();
        gameObject.SetActive(false);
    }

    private void PlayDialouge(string dialouge)
    {
        IEnumerator TextDuration(float delay)
        {
            yield return new WaitForSeconds(delay);
            dialogueTMP.transform.parent.gameObject.SetActive(false);
            // StartCoroutine(FadeText());
        }

        if (!speechBubbleObj.activeSelf) speechBubbleObj.SetActive(true);
        dialogueTMP.color = originalTextColor;
        dialogueTMP.text = dialouge;
        // StartCoroutine(TextDuration(1f));
    }

    private IEnumerator FadeText()
    {
        float r = originalTextColor.r;
        float g = originalTextColor.g;
        float b = originalTextColor.b;
        float a = originalTextColor.a;
        float decreaseRate = a / 10f;
        while (dialogueTMP.color.a > 0)
        {
            a -= decreaseRate;
            dialogueTMP.color = new Color(r, g, b, a);
            yield return null;
        }
    }

    public void Pay(int chargedAmount)
    {
        int actualOrderCost = customerSO.GetFoodOrderTotal();
        if (chargedAmount > actualOrderCost)
        {
            if (overchargeDialouge != "") PlayDialouge(overchargeDialouge);
            else PlayDialouge("Why did you charge me so much! Whatever, take it.");
            PlayerDataManager.Instance.IncreaseCorpoEndingCounter(chargedAmount - actualOrderCost);

            angryDialogueInstance.start();
        }
        else if (chargedAmount < actualOrderCost)
        {
            if (discountDialouge != "") PlayDialouge(discountDialouge);
            else PlayDialouge("Thanks for the discount!");
            PlayerDataManager.Instance.IncreaseGoodEndingCounter(actualOrderCost - chargedAmount);

            happyDialogueInstance.start();
        }
        else
        {
            if (outroDialouge != "") PlayDialouge(outroDialouge);
            else PlayDialouge("Ah you charged me just the right amount!");

            neutralDialogueInstance.start();
        }
    }

    public int GetFoodOrderTotal()
    {
        return customerSO.GetFoodOrderTotal();
    }
}
