
using UnityEngine;

public class CrainToCarConnector : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRb;
    [SerializeField] private CraneSpawner _spawner; 

    private void OnEnable()
    {
        _spawner.PartSpawned += OnCraneSpawned;
    }

    private void OnDisable()
    {
        _spawner.PartSpawned -= OnCraneSpawned;

    }
    private void OnCraneSpawned(Crane part)
    { // crane update??
        Crane crane = part;

        if (crane != null) {
           
            crane.ConnectToBody(_carRb);
        }      
    }
}
