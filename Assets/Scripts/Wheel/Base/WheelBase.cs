
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class WheelBase : MonoBehaviour
{
    [SerializeField] private List<IWheelsSpawner> _spawners;

    [SerializeField] private List<WheelUpgrade> _wheelPrefabs;

    private void Awake()
    {
        _spawners = GetComponents<IWheelsSpawner>().ToList();
    }

    private void Start()
    {
        List<WheelUpgrade> wheels = new List<WheelUpgrade>();

        foreach (WheelUpgrade wheel in _wheelPrefabs)
        {
            for (int i = 0; i < wheel.Count; i++)
            {
                var obj = Instantiate(wheel);

                foreach (var spawner in _spawners)
                {
                    if (spawner.TrySpawn(obj))
                    {
                        Debug.Log("Spawn");
                    }
                    else
                    {
                        Debug.Log("Not spawn");
                    }
                }
            }
        }

    }
}
