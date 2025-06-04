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
        foodObject.UpdateText();

        Vector2 pos = Vector2.zero;
        float xOffset = -boundaryBox.x / 2f + 0.5f;
        float yOffset = boundaryBox.y / 2f - 0.5f;
        float xCellSize = boundaryBox.x / 4f;
        float yCellSize = boundaryBox.y / 2f;
        pos.x += xCellSize * ((activeObjects.Count-1) % 4f) + xOffset;
        pos.y += -yCellSize * ((activeObjects.Count-1) / 4) + yOffset;
        foodObject.transform.localPosition = pos;

        if (foodSO.Sprite != null)
        {
            foodObject.GetComponent<SpriteRenderer>().sprite = foodSO.Sprite;
            foodObject.transform.localScale = Vector2.one * foodSO.Scale;
            
            List<Vector2> newPointsList = new();
            Sprite spriteRef = foodObject.GetComponent<SpriteRenderer>().sprite;
            spriteRef.GetPhysicsShape(0, newPointsList);
            foodObject.GetComponent<PolygonCollider2D>().points = newPointsList.ToArray();
        }
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
