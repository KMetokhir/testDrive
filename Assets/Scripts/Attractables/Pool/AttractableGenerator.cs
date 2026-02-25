using UnityEngine;

public abstract class AttractableGenerator<T> : MonoBehaviour // 
 where T : Attractable
{

    [SerializeField] private ObjectPool<T> _pool;

    public T Generate()
    {
        T attractable = _pool.GetObject(); 

        return attractable;
    }
}


