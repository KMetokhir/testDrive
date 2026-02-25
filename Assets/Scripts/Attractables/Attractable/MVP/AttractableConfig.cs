using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Attractable Config")]
public class AttractableConfig : ScriptableObject
{
    [SerializeField] private  AttractablesType _type;
    [SerializeField] private uint _weight;
    [SerializeField] private uint _cost;
    [SerializeField] private uint _level;

    public AttractablesType Type => _type;
    public uint Weight => _weight;
    public uint Cost => _cost;
    public uint Level => _level;

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
}