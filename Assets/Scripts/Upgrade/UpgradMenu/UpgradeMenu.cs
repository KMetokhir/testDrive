using UnityEngine;

public  class UpgradeMenu : MonoBehaviour
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