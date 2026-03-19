using System;
using UnityEngine;

public abstract class Settings<TUpgrader, TData> : MonoBehaviour
    where TData : IUpgradeData
    where TUpgrader : MonoBehaviour, IUpgrader<TData>
{
    [SerializeField] private TUpgrader _upgrader;

    public event Action Changed;

    private void Awake()
    {
        _upgrader = GetComponentInChildren<TUpgrader>();

        if (_upgrader == null)
        {
            throw new Exception($"Upgrader {typeof(TUpgrader).ToString()} == null");
        }
    }

    private void OnEnable()
    {
        _upgrader.UpgradeExecuted += SetNewStats;
    }

    private void OnDisable()
    {
        _upgrader.UpgradeExecuted -= SetNewStats;
    }

    private void SetNewStats(TData data)
    {
        ApplyUpgrade(data);
        Changed?.Invoke();
    }

    protected abstract void ApplyUpgrade(TData data);
}
