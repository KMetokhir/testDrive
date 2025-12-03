using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSettings : MonoBehaviour
{
    [SerializeField] private MagnetUpgrader _upgrader;

    public float MagnetRadius { get; private set; }

  

    private void OnEnable()
    {
        _upgrader.UpgradeExecuted += SetNewStats;

    }

    private void SetNewStats(IMagnetUpgradeData data)
    {
        MagnetRadius = data.MagnetRadius;
    }

    private void OnDisable()
    {
        _upgrader.UpgradeExecuted -= SetNewStats;
    }
}
