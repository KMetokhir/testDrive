using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WheelsUpgradeSpawner : GenericUpgradeSpawner<WheelUpgradePart>
{
    public abstract event Action<WheelUpgradePart> WheelSpawned;
}
