using UnityEngine;
using Zenject;

public class AttractablesRemover<T> : PoollableRemover<T>
    where T : Attractable
{
    [SerializeField] private AttractableDataHandler<T> _dataHandler;

}