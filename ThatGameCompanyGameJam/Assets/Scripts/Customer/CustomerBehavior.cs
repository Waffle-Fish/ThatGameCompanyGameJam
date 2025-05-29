using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    [SerializeField] CustomerScriptableObject customerSO;
    private SpriteRenderer spriteRenderer;
    private FoodSpawner foodSpawner;

    private TextMeshPro dialogueTMP;
    private Color originalTextColor;
    // float MoneyOnHand = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foodSpawner = FoodSpawner.Instance;
        dialogueTMP = GetComponentInChildren<TextMeshPro>();
        originalTextColor = dialogueTMP.color;
        customerSO.InitializeFoodOrder();
    }

    private void OnEnable()
    {
        // if (customerSO != null) DisplayOrder();
        DisplayOrder();
    }

    private void OnDisable()
    {
        ProcessLeave();
    }

    public void DisplayOrder()
    {
        foreach (var order in customerSO.FoodOrder)
        {
            foodSpawner.PlaceFood(order.FoodSO);
        }
        PlayDialouge("I'll have these please");
    }

    private void ProcessLeave()
    {
        foodSpawner.ReleaseAllActive();
    }

    private void PlayDialouge(string dialouge)
    {
        IEnumerator TextDuration(float delay)
        {
            yield return new WaitForSeconds(delay);
            dialogueTMP.transform.parent.gameObject.SetActive(false);
            // StartCoroutine(FadeText());
        }

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
