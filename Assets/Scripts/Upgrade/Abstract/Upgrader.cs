using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class Upgrader<T, S, M> : MonoBehaviour
    where S : ButtonWithSliderView
    where M : IUpgradeData
    where T : Upgrade, M
{
    [SerializeField] private List<T> _upgrades;
    [SerializeField] private List<UpgradePartSpawner> _spawners;

    [SerializeField] private S _view;
    [SerializeField] private Money _money;

    [SerializeField] private List<UpgradePartSpawner> _compositePartSpawners;  // Observable part spawner
                                                                               // [SerializeField] private List<ObservableUpgradePart> _observableParts;

    private ICarLevel _carLevel;

    [SerializeField] private List<UpgradePart> _installedUpgradeParts;

    private T _currentUpgrade;

    public event Action<M> UpgradeExecuted;

    private void Start()
    {
        _compositePartSpawners = new List<UpgradePartSpawner>();
        _installedUpgradeParts = new List<UpgradePart>();

        _carLevel = FindObjectOfType<Car>();

        _currentUpgrade = FindUpgrade(1, 0);// tmp

        SpawnParts(_currentUpgrade);

        /* foreach (UpgradePart upgradePart in _installedUpgradeParts)
         {
             upgradePart.DestroyObject();
         }

         List<UpgradePart> newParts = _currentUpgrade.Execute();

         foreach (UpgradePart part in newParts)
         {
             foreach (UpgradePartSpawner spawner in _spawners)
             {
                 if (spawner.TrySpawn(part))
                 {
                     RemoveOldPart(spawner, part);
                     _installedUpgradeParts.Add(part);
                     break;
                 }
             }
         }*/

        UpgradeExecuted?.Invoke((M)_currentUpgrade);

        _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());
    }

    private void RemoveOldPart(UpgradePartSpawner spawner)
    {
        for (int i = _installedUpgradeParts.Count - 1; i >= 0; i--)
        {
            if (spawner.IsSpawnPossible(_installedUpgradeParts[i]))
            {
                // UpgradePart part = _installedUpgradeParts[i];
                //_installedUpgradeParts[i].DestroyObject();

                var part = _installedUpgradeParts[i];

                _installedUpgradeParts.RemoveAt(i);  // remove all or only one????

                part.DestroyObject();


                break;

                //Destroy(_installedUpgradeParts[i].gameObject);               

            }
        }

        /*foreach (UpgradePart oldPart in _installedUpgradeParts)
        {
            if (spawner.IsSpawnPossible(oldPart))
            {
                _installedUpgradeParts.Remove(oldPart);
                oldPart.DestroyObject();
            }
        }*/
    }

    private void OnEnable()
    {
        _view.UpgradeButtonClicked += Upgrade;
    }

    /*private void Start()
    {
        *//*UpgradeExecuted?.Invoke((M)_currentUpgrade);

        _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());*//*
    }*/

    private void Upgrade()
    {
        if (_upgrades == null || _upgrades.Count == 0)
        {
            Debug.Log((_upgrades.Count == 0) + " " + _upgrades == null);
            throw new NullReferenceException(nameof(_installedUpgradeParts));
        }

        uint nextLevelUpgrade = _currentUpgrade.UpgradeLevel + 1;// tmp

        T upgrade = FindUpgrade(_carLevel.Level, nextLevelUpgrade);
        bool isExists = upgrade != null;

        if (isExists)
        {
            if (_money.TryDecrease(upgrade.Cost))
            {
                _currentUpgrade = upgrade;

                SpawnParts(_currentUpgrade);
                /*if (_installedUpgradeParts.Count != newParts.Count)
                {
                    throw new Exception("Unknown upgrade part, need spawner");
                }*/

                UpgradeExecuted?.Invoke((M)_currentUpgrade);

                _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());
            }
        }
    }

    private void SpawnParts(Upgrade upgrade)
    {
        List<UpgradePart> newParts = _currentUpgrade.Execute();



        List<UpgradePart> installedParts = new List<UpgradePart>();

        /* while (newParts.Count > 0) { 



         }

         foreach (UpgradePart part in newParts) { 

             for

         }*/


        for (int i = newParts.Count - 1; i >= 0; i--)
        {
            foreach (UpgradePartSpawner spawner in _spawners)
            {
                if (spawner.TrySpawn(newParts[i]))
                {
                    RemoveOldPart(spawner);
                    ProcessCompositPart(newParts[i]);

                    installedParts.Add(newParts[i]);
                    newParts.RemoveAt(i);

                    break;
                }
            }
        }



        for (int i = newParts.Count - 1; i >= 0; i--) // unite in one circle with spawn below
        {
            foreach (UpgradePartSpawner spawner in _compositePartSpawners)
            {
                if (spawner.TrySpawn(newParts[i]))
                {

                    RemoveOldPart(spawner);

                    ObservableUpgradePart observablePart = newParts[i] as ObservableUpgradePart;

                    if (observablePart == null)
                    {
                        throw new Exception("Part is not observable");
                    }

                    //_observableParts.Add(observablePart);
                    installedParts.Add(newParts[i]);
                    newParts.RemoveAt(i);



                    observablePart.Destroied += RemoveObservablePart;


                    break;
                }
            }
        }

        _installedUpgradeParts.AddRange(installedParts);


        if (newParts.Count != 0)
        {
            throw new Exception("Some upgrade parts not Spawned ");
        }
    }

    private void RemoveObservablePart(ObservableUpgradePart part)
    {
        Debug.Log("REMOVE parts from list in upgrade");
        // _observableParts.Remove(part);

        _installedUpgradeParts.Remove(part);
    }

    private void ProcessCompositPart(UpgradePart part)
    {
        CompositePart compositePart = part as CompositePart;

        if (compositePart != null)
        {
            _compositePartSpawners.AddRange(compositePart.GetSpawners());
            compositePart.Destroied += OnCompositePartDestroied;
        }
    }

    private void OnCompositePartDestroied(ObservableUpgradePart part)
    {
        part.Destroied -= OnCompositePartDestroied;

        CompositePart compositePart = part as CompositePart;

        if (compositePart != null)
        {
            _compositePartSpawners = _compositePartSpawners.Except(compositePart.GetSpawners()).ToList();
        }
        else
        {
            throw new Exception("Incorrect input data " + nameof(part));
        }
    }


    private uint GetMaxUpgradeLevel()
    {
        return _upgrades.Max(upgrade => upgrade.UpgradeLevel);
    }

    private T FindUpgrade(uint carLevel, uint upgradeLevel)
    {
        return _upgrades.FirstOrDefault(part => part.CarLevel == carLevel && part.UpgradeLevel == upgradeLevel);
    }
}