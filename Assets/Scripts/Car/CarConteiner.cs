using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class CarConteiner : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void DoSmth()
    {
        Debug.Log("Do smth in " + rb.transform.position); // tmp tests
        
    }
    /*public class Factory : PlaceholderFactory<CarConteiner>
    {
    }*/
}
