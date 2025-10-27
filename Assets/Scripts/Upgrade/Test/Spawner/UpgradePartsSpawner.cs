using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public abstract class UpgradePartsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    /* [SerializeField] private Transform _crane;
       [SerializeField] private Transform _head;
       [SerializeField] private Transform _frontRightWheel;
       [SerializeField] private Transform _frontLeftWheel;
       [SerializeField] private Transform _backRightWheel;*/

    public virtual bool TrySpawn(UpgradePart part)
    {
        if (IsSpawnPossible(part))
        {
            part.transform.position = _parent.TransformPoint(part.SpawnPosition);
            part.transform.rotation = _parent.transform.rotation;
            part.transform.parent = _parent;

            return true;
        }

        return false;
    }

    public abstract bool IsSpawnPossible(UpgradePart part);


    /*  private Transform DetermineParent(UpgradePart part)
      {
          return part switch
          {
              BodyPart => _carBody,
              CranePArt => _crane,
              _ => throw new Exception("Undefined parent ")
          };
      }*/
}
