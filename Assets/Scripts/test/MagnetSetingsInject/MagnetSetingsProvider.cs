using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSetingsProvider : IMagnetData
{
    private IMagnetData _currentData;
    public float MagnetRadius => _currentData.MagnetRadius;

    public void Set(IMagnetData magnetData)
    {
        _currentData = magnetData ?? throw new System.Exception($"{nameof(magnetData)} is null"); 
    }
}
