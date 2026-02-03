using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Attractable : MonoBehaviour, IAttractable, IAttractableLevel
{
    //[SerializeField] private float _toleranceDistance;
    [SerializeField] private uint _weight;
    [SerializeField] private uint _cost;
    [SerializeField] private uint _level;
    [SerializeField] private AttractablesType _type;

    private string _id;

    private Vector3 _currentPosition;

    public bool IsActive { get; private set; }
    public Transform Transform => transform;
    public uint Weight => _weight;
    public uint Cost => _cost;
    public uint Level => _level;
    public string Id => _id;

    public AttractablesType Type => _type;

    public event Action<Attractable> Deactivated;
    public event Action<Attractable> Collected;



    private void OnValidate()
    {
        if (_weight == 0)
        {
            throw new Exception("Weght = 0");
        }

        if (_cost == 0)
        {
            throw new Exception("Cost = 0");
        }
    }

    private void Awake()
    {
        _id = GenerateUniqueId();
        IsActive = true;
        _currentPosition = transform.position;
    }

    public void Deactivate()
    {
        IsActive = false;
        Deactivated?.Invoke(this);
    }

    public void Collect()
    {
        Collected?.Invoke(this);
    }


    private string GenerateUniqueId()
    {
        string time = DateTime.Now.Ticks.ToString();
        string random = UnityEngine.Random.Range(1000, 9999).ToString();
        return $"{_type}_{time}_{random}";
    }


}

public enum AttractablesType
{
    screw,
    wrench
}