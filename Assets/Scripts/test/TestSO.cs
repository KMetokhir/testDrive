using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TESTSO", menuName = "TESTSO")]
public class TestSO : ScriptableObject
{
    [SerializeField] private int value;
    public void SubscribeToMonoBehaviour(SOUser monoBehaviour)
    {
        monoBehaviour.ChangeValue += OnChangeValue;
    }

    public void UnsubscribeFromMonoBehaviour(SOUser monoBehaviour)
    {
        monoBehaviour.ChangeValue -= OnChangeValue;
    }

    private void OnChangeValue(int newValue)
    {
        value = newValue;
        Debug.Log($"ScriptableObject value updated to {value}");
    }
}
