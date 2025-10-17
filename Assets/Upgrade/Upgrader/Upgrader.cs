using UnityEngine;

public  class Upgrader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out UpgradeAria seller))
        {
            Debug.Log("In upgrade aria");
        }
    }
}