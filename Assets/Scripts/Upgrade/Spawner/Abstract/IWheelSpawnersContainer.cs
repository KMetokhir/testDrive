using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWheelSpawnersContainer 
{
    public event Action<WheelUpgradePart> PartSpawned;
}
