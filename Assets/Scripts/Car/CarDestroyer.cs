using UniRx;
using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    public void Destroy()
    {
        MessageBroker.Default.Publish(new CarDestroied
        {            
        });

        Destroy(gameObject);
    }

    public struct CarDestroied
    {      
    }
}

   
