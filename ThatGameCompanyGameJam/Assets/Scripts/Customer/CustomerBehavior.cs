using System.Collections.Generic;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    [SerializeField] CustomerScriptableObject customerSO;
    private List<FoodScriptableObject> Order;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        if (customerSO.CustomerSprite) spriteRenderer.sprite = customerSO.CustomerSprite;
    }

    void Update()
    {

    }

    private void DisplayOrder()
    {
        
    }
}
