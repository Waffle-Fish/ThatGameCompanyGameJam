using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class CustomerBehavior : MonoBehaviour
{
    [SerializeField] CustomerScriptableObject customerSO;
    [SerializeField] AnimationClip enterAnimation;
    [SerializeField] AnimationClip exitAnimation;

    [SerializeField] EventReference doorReference;
    [SerializeField] EventReference walkingReference;
    [SerializeField] EventReference dialogueReference;

    private EventInstance doorInstance;
    private EventInstance walkingInstance;
    private EventInstance dialogueInstance;

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
        dialogueInstance = RuntimeManager.CreateInstance(dialogueReference);

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
        PlayDialouge("I'll have these please");
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

        dialogueInstance.start();
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
        if (chargedAmount > actualOrderCost + 1)
        {
            Debug.Log("Why did you charge me so much! Whatever, take it.");
            PlayDialouge("Why did you charge me so much! Whatever, take it.");
        }
        else if (chargedAmount < actualOrderCost - 1)
        {
            Debug.Log("Thanks for the discount!");
            PlayDialouge("Thanks for the discount!");
        }
        else
        {
            Debug.Log("Ah you charged me just the right amount!");
            PlayDialouge("Ah you charged me just the right amount!");
        }
    }
}
