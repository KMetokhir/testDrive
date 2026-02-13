using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class CarConteiner : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CarCompositDestroier _carDestroier;   
 
    public void Destroy()
    {       
        _carDestroier.Destroy();        
    }
}
