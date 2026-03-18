using System;
using UnityEngine;

public class TrunkSettings : MonoBehaviour, ITrunkData
{
    [SerializeField] private PowerUpgrader _upgrader;

    public event Action Changed;

    public uint MaxWeight { get; private set; }
    private void OnEnable()
    {
        _upgrader.UpgradeExecuted += SetNewStats;
    }

    private void OnDisable()
    {
        _upgrader.UpgradeExecuted -= SetNewStats;
    }

    private void SetNewStats(IPowerUpgradeData data)
    {
        MaxWeight = data.MaxWeight;
        Changed?.Invoke();
    }
}
