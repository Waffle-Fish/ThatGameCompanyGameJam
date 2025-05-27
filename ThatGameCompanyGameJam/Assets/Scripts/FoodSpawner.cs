using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance { get; private set; }
    [SerializeField] private FoodBehavior foodPrefab;
    [Tooltip("Width & height of where food can spawn. Center is transform.position")]

    [SerializeField][Min(0f)] private Vector2 boundaryBox;

    private IObjectPool<FoodBehavior> objectPool;
    private List<FoodBehavior> activeObjects;

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private void Awake()
    {
        objectPool = new ObjectPool<FoodBehavior>(CreateFood, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        activeObjects = new();
    }

    private FoodBehavior CreateFood()
    {
        FoodBehavior foodInstance = Instantiate(foodPrefab);
        foodInstance.ObjectPool = objectPool;
        foodInstance.transform.parent = transform;
        return foodInstance;
    }

    private void OnGetFromPool(FoodBehavior pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
        activeObjects.Add(pooledObject);
    }

    private void OnReleaseToPool(FoodBehavior pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(FoodBehavior pooledObject)
    {
        DestroyImmediate(pooledObject);
    }

    public void PlaceFood(FoodScriptableObject foodSO)
    {
        FoodBehavior foodObject = objectPool.Get();
        foodObject.FoodSO = foodSO;

        Vector2 pos = transform.position;
        float xOffset = Random.Range(-boundaryBox.x / 2f, boundaryBox.x / 2f);
        float yOffset = Random.Range(-boundaryBox.y / 2f, boundaryBox.y / 2f);
        pos.x += xOffset;
        pos.y += yOffset;
        foodObject.transform.position = pos;
    }

    public void ReleaseAllActive()
    {
        if (activeObjects.Count == 0) return;
        foreach (var obj in activeObjects)
        {
            obj.ReleaseFood();
        }
        activeObjects.Clear();
    }
}
