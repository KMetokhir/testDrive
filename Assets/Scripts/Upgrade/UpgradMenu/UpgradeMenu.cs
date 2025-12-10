using UnityEngine;
using Zenject;

public  class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private UpgradePanel _upgraderPanel;

    [Inject]
    private void Construct(UpgradePanel upgraderPanel)
    {
        _upgraderPanel = upgraderPanel;
    }

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