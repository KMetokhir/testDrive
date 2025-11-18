using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private DrivenWheelsSpawner spawner;
    [SerializeField] private LeftRotaryWheelUpgrade wheel;

    private void Awake()
    {
       UpgradePart patrt = wheel as UpgradePart;
        UpgradePartSpawner sp = spawner;        

        

        if (sp.IsSpawnPossible(wheel))
        {
            Debug.Log("TRUU");
        }
        else
        {
            Debug.Log("NOOT SPAAWM");
        }
    }
}
