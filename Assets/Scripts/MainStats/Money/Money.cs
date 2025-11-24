using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private uint _stratValue;
    [SerializeField] private MoneyView _view;

    public uint Value { get; private set; }

    private void Start()
    {
        Value = _stratValue;
        _view.Show(Value);
    }

    public bool TryDecrease(uint cost)
    {
        if (Value >= cost)
        {
            Value -= cost;

            _view.Show(Value);

            return true;
        }

        return false;
    }

    public void Increase(uint value)
    {
        Value += value;
        _view.Show(Value);
    }
}
