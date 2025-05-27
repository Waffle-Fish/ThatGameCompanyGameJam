using UnityEngine;
using UnityEngine.Pool;

public class FoodBehavior : MonoBehaviour
{
    public FoodScriptableObject FoodSO;

    private IObjectPool<FoodBehavior> objectPool;
    public IObjectPool<FoodBehavior> ObjectPool { set => objectPool = value; }

    public void ReleaseFood()
    {
        objectPool.Release(this);
    }
}
