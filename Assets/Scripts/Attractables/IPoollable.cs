
using UnityEngine;

public interface IPoollable
{
    public Transform Transform { get; }
    public void Activate();
    public void Deactivate();
}


// change collect system in shop? pool object disapier when collect, in shop check when i point and deactivate, whhen load to another scene objects in shop destroys but pool try to get it