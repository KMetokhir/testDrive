using UnityEngine;

public abstract class PoolableGenerator<T> : MonoBehaviour
 where T : MonoBehaviour, IPoollable //Attractable
{
    [SerializeField] private ObjectPool<T> _pool;

    public T Generate()
    {
        T attractable = _pool.GetObject();

        return attractable;
    }
}