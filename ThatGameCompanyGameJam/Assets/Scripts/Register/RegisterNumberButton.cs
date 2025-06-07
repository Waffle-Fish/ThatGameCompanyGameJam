using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegisterNumberButton : CustomButton
{
    [SerializeField][Range(0, 10)] private int keyValue;
    public int KeyValue { get { return keyValue; } private set { keyValue = value; } }

    public static Action<int> OnRegisterKeyPressed;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        OnRegisterKeyPressed?.Invoke(KeyValue);
    }
}
