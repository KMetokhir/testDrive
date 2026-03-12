using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSettings : MonoBehaviour, IMagnetData
{
    [SerializeField] private MagnetUpgrader _upgrader;

    public float MagnetRadius { get; private set; }

    private void OnEnable()
    {
        _upgrader.UpgradeExecuted += SetNewStats;
    }

    private void OnDisable()
    {
        _upgrader.UpgradeExecuted -= SetNewStats;
    }

    private void SetNewStats(IMagnetData data)
    {
        MagnetRadius = data.MagnetRadius;
    }
}
