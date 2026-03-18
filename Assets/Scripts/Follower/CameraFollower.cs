using UnityEngine;
using Zenject;

public class CameraFollower : MonoBehaviour // test
{
    [Inject]
    private ICarBody _car;

    private void FixedUpdate()
    {
        if (_car.Rigidbody != null)
        {
           // Debug.LogError("Worke");
        }
    }
}