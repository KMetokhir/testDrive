
using UnityEngine;

public interface IPoollable
{
    public Transform Transform { get; }
    public void Activate();
    public void Deactivate();
}
