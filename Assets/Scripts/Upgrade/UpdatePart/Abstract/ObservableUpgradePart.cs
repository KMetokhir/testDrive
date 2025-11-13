using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObservableUpgradePart : UpgradePart
   
{
    public event Action<UpgradePart> Destroied;

    private void OnDestroy()
    {
        Destroied?.Invoke(this);
    }
}
