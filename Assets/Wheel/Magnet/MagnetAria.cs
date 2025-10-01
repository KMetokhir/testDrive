using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagnetAria : MonoBehaviour
{
    [SerializeField] private float _magnetRadius = 0.2f;
    [SerializeField] private float _detectInterval=0.2f;

    private Coroutine _detectCoroutine;
    private bool _isWork = false;

    public event Action<List<IAttractable>> AttractableObjectsFound;

    private void Start()
    {
        StartDetecting();
    }

    private void OnDisable()
    {
        StopDetecting();
    }

    private void StartDetecting()
    {
        if (_isWork || _detectCoroutine != null)
        {
            return;
        }

        _isWork = true;
        _detectCoroutine = StartCoroutine(Detect());
    }

    private void StopDetecting()
    {
        _isWork = false;

        StopCoroutine(_detectCoroutine);
        _detectCoroutine = null;
    }


    private IEnumerator Detect()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_detectInterval);

        while (_isWork)
        {
            if (TryGetAttractable(out List<IAttractable> attractables))
            {
                attractables = GetActiveObjects(attractables);

                if (attractables != null && attractables.Count>0)
                {
                    AttractableObjectsFound?.Invoke(attractables);                    
                }
            }

            yield return waitingTime;
        }
    }

    private List<IAttractable> GetActiveObjects(List<IAttractable> attractable)
    {
        return attractable.Where(attractable => attractable.IsActive).ToList();
    }

    private bool TryGetAttractable(out List<IAttractable> attractables)
    {
        bool isDetected = false;
        attractables = new List<IAttractable>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _magnetRadius);

        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent(out IAttractable attractable))
            {
                attractables.Add(attractable);
                isDetected = true;
            }
        }

        return isDetected;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _magnetRadius);
    }
}
