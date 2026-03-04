using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSetingsProvider : IMagnetUpgradeData
{
    private IMagnetUpgradeData _currentData;
    public float MagnetRadius => _currentData.MagnetRadius;

    public void Set(IMagnetUpgradeData magnetData)
    {
        _currentData = magnetData ?? throw new System.Exception($"{nameof(magnetData)} is null"); 
    }
}
