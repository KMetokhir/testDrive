using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsUpgradeSpawner : GenericUpgradeSpawner<WheelPart>
{
    [SerializeField] private List<WheelSpawner> _wheelSpawners;

    public override bool TrySpawn(UpgradePart part)
    {
        foreach(WheelSpawner wheelSpawner in _wheelSpawners)
        {
            if(wheelSpawner.TrySpawn(part)== false)
            {
                return false;
            }
            
        }

        return true;
    }

}
