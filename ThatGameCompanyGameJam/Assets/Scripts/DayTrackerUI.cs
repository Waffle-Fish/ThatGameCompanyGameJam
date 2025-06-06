using System.Collections;
using System.Threading;
using UnityEngine;

public class DayTrackerUI : MonoBehaviour
{
    private float rotationPerCustomer = 0;
    Transform arrow;
    Coroutine rotateCoR;

    private void Awake()
    {
        arrow = transform.GetChild(0);
    }

    private void OnEnable()
    {
        GameManager.OnSpawnNextCustomer += ProcessNextCustomer;
        CustomersManager.OnDespawnCurrentCustomer += ForceSetTracker;
    }

    private void OnDisable()
    {
        GameManager.OnSpawnNextCustomer -= ProcessNextCustomer;
        CustomersManager.OnDespawnCurrentCustomer -= ForceSetTracker;
    }

    void Start()
    {
        rotationPerCustomer = -180f / (float)CustomersManager.Instance.transform.childCount;
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
}
