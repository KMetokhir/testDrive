using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgraderView : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;

    [SerializeField] private Slider _slider;// same as trunkView

    public event Action UpgradeButtonClicked;

    private void Awake()
    {
        if (_slider == null)
        {
            throw new NullReferenceException(nameof(_slider));
        }

        if (UpgradeButtonClicked == null)
        {
            throw new NullReferenceException(nameof(_upgradeButton));
        }
    }

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(OnButtonClick);
    }

    public void ShowValue(uint value, uint maxValue)
    {
        _slider.minValue = 0f;
        _slider.maxValue = maxValue;
        _slider.wholeNumbers = true;

        _slider.value = value;
    }

    private void OnButtonClick()
    {
        UpgradeButtonClicked?.Invoke();
    }
}
