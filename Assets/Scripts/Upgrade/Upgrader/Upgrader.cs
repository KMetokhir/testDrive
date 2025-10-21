using UnityEngine;

public  class Upgrader : MonoBehaviour
{
    [SerializeField] private UpgradePanel _upgraderPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out UpgradeAria seller))
        {
            _upgraderPanel.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out UpgradeAria seller))
        {
            _upgraderPanel.Hide();
        }
    }
}