using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpVeiw : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;

    [SerializeField] private Slider _slider;

    public event Action ButtonClicked;

    private void Awake()
    {
        if (_slider == null)
        {
            throw new NullReferenceException(nameof(_slider));
        }

        if (_upgradeButton == null)
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

    public void ActivateButton()
    {
        _upgradeButton.gameObject.SetActive(true);
    }

    public void DeactivateButton()
    {
        _upgradeButton.gameObject.SetActive(false);
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
        ButtonClicked?.Invoke();
    }
}
