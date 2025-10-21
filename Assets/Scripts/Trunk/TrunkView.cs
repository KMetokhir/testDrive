using System;
using UnityEngine;
using UnityEngine.UI;

public class TrunkView : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null)
        {
            throw new NullReferenceException(nameof(_slider));
        }
    }

    public void ShowValue(uint value, int maxValue)
    {
        _slider.minValue = 0f;
        _slider.maxValue = maxValue;
        _slider.wholeNumbers = true;

        _slider.value = value;
    }
}
