using UnityEngine;

public class UpgradePart : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnLocalPosition;

    public Vector3 SpawnPosition => _spawnLocalPosition;

    public UpgradePart Clone()
    {

        UpgradePart clone = Instantiate(this.gameObject).GetComponent<UpgradePart>();

        return clone;
    }
}
