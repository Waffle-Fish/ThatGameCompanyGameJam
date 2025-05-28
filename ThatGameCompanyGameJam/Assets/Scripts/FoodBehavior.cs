using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class FoodBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float TextYOffset = 0.725f;
    public FoodScriptableObject FoodSO;

    private IObjectPool<FoodBehavior> objectPool;
    public IObjectPool<FoodBehavior> ObjectPool { set => objectPool = value; }

    private List<TextMeshPro> tmps;

    const float TEXT_SCALE = 0.1f;

    private void Awake()
    {
        tmps = new();
        GetComponentsInChildren<TextMeshPro>(true, tmps);
    }

    private void Start()
    {
        name = FoodSO.name;
        UpdateText();
    }

    public void UpdateText()
    {
        foreach (var tmp in tmps)
        {
            tmp.text = FoodSO.name;
            tmp.rectTransform.localScale = Vector3.one * TEXT_SCALE / transform.localScale.x;
            tmp.rectTransform.position = new(tmp.rectTransform.position.x, tmp.rectTransform.position.y + TextYOffset);
        }
    }

    public void ReleaseFood()
    {
        objectPool.Release(this);
    }

    public void EnableTexts()
    {
        foreach (var tmp in tmps)
        {
            tmp.gameObject.SetActive(true);
        }
    }

    public void DisableTexts()
    {
        foreach (var tmp in tmps)
        {
            tmp.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse has entered " + name);
        EnableTexts();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse has exited " + name);
        DisableTexts();
    }
}
