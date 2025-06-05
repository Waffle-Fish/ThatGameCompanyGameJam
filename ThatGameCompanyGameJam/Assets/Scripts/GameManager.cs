using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField][Min(1)] private int quota = 0;
    public int Quota { get => quota; private set => quota = value; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
