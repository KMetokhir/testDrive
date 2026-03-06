using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class CarConteiner : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CarDestroyer _carDestroier;
    [SerializeField] private LevelUpSystem _levelup;

   // public uint Value => _levelup.Value;

    public void Destroy()
    {       
        _carDestroier.Destroy();        
    }
}
