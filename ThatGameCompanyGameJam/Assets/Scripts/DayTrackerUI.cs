using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class DayTrackerUI : MonoBehaviour
{
    private float rotationPerCustomer = 0;
    Transform arrow;
    Coroutine rotateCoR;
    List<TextMeshProUGUI> tmps;
    int CurrentRevenue = 0;

    private void Awake()
    {
        arrow = transform.GetChild(0);
        tmps = new();
        GetComponentsInChildren(tmps);
    }

    private void OnEnable()
    {
        GameManager.OnSpawnNextCustomer += ProcessNextCustomer;
        CustomersManager.OnDespawnCurrentCustomer += ForceSetTracker;
        CustomersManager.OnUpdateCurrentRevenue += UpdateCurrentRevenueText;
    }

    private void OnDisable()
    {
        GameManager.OnSpawnNextCustomer -= ProcessNextCustomer;
        CustomersManager.OnDespawnCurrentCustomer -= ForceSetTracker;
        CustomersManager.OnUpdateCurrentRevenue += UpdateCurrentRevenueText;
    }

    void Start()
    {
        rotationPerCustomer = -180f / (float)CustomersManager.Instance.transform.childCount;
        tmps[1].text = GameManager.Instance.DailyQuota.ToString();
        tmps[0].color = Color.red;
    }

    private void ProcessNextCustomer()
    {
        rotateCoR = StartCoroutine(RotateTracker());
    }

    private void ForceSetTracker(int revenue)
    {
        StopCoroutine(rotateCoR);
        float newAngle = 90f + rotationPerCustomer * (CustomersManager.Instance.CurrentCustomerIndex + 1);
        arrow.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    IEnumerator RotateTracker()
    {
        float timer = 0;
        float duration = 15f;
        while (timer < duration)
        {
            arrow.Rotate(0f, 0f, rotationPerCustomer * Time.deltaTime / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
    }

    private void UpdateCurrentRevenueText(int money)
    {
        CurrentRevenue += money;
        tmps[0].text = CurrentRevenue.ToString();
        float percentage = CurrentRevenue / (float)GameManager.Instance.DailyQuota;
        Color newColor = Color.Lerp(Color.red, Color.green, percentage);
        tmps[0].color = newColor;
    }
}
