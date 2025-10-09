using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrader : MonoBehaviour
{
    [SerializeField] private List<SpeedUpgrade> _upgrades;
    [SerializeField] private Transform _carBody;

    private List<UpgradePart> _installedUpgradeParts;

    private void Awake()
    {

        _installedUpgradeParts = new List<UpgradePart>();
    }

    private void Upgrade(uint level)
    {
        if (_upgrades == null || _upgrades.Count == 0)
        {
            Debug.Log((_upgrades.Count == 0) + " " + _upgrades == null);
            throw new NullReferenceException(nameof(_installedUpgradeParts));
        }

        foreach (UpgradePart upgradePart in _installedUpgradeParts)// find equal upgrades from new and installed
        {
            GameObject.Destroy(upgradePart.gameObject);
        }

        if (_installedUpgradeParts != null)
        {
            _installedUpgradeParts.Clear();
        }

        List<UpgradePart> newParts = _upgrades[(int)level].GetUpgradeParts();// temp

        foreach (UpgradePart item in newParts)
        {
            // Instantiate(item);
            item.transform.position = _carBody.TransformPoint(item.SpawnPosition);
            item.transform.rotation = _carBody.transform.rotation;
            item.transform.parent = _carBody;

        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Upgrade(0);
        }
    }
}
