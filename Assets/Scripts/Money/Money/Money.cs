using UnityEngine;
using Zenject;

public class Money : MonoBehaviour
{
    [SerializeField] private uint _stratValue;
    [SerializeField] private MoneyView _view;

    private MoneySaver _saver;

    public uint Value { get; private set; }

    [Inject]
    private void Construct(MoneyView view, MoneySaver saver)
    {
        _view = view;
        _saver = saver;
    }

    private void Start()
    {
        Value = _saver.GetMoney(_stratValue);
        _view.Show(Value);
    }

    public bool TryDecrease(uint cost)
    {
        if (Value >= cost)
        {
            Value -= cost;

            _view.Show(Value);
            _saver.SaveMoney(Value);

            return true;
        }

        return false;
    }

    public void Increase(uint value)
    {
        Value += value;
        _view.Show(Value);
        _saver.SaveMoney(Value);
    }   
}