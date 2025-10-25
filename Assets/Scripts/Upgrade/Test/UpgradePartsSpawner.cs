using System;
using UnityEngine;

public class UpgradePartsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _carBody;
    [SerializeField] private Transform _crane;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _frontRightWheel;
    [SerializeField] private Transform _frontLeftWheel;
    [SerializeField] private Transform _backRightWheel;

    public void Spawn(UpgradePart part)
    {
        Transform parent = DetermineParent(part);
        part.transform.position = parent.TransformPoint(part.SpawnPosition);
        part.transform.rotation = parent.transform.rotation;
        part.transform.parent = parent;
    }

    private Transform DetermineParent(UpgradePart part)
    {
        return part switch
        {
            BodyPart => _carBody,
            CranePArt => _crane,
            _ => throw new Exception("Undefined parent ")
        };
    }
}
