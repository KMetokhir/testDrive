using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using static CarDestroyer;
using static UnityEditor.Progress;

public class CarPlacer : MonoBehaviour
{
    [SerializeField] private float _yOffset;
    //  [SerializeField] private CarCompositDestroier _carDestroier;
    [SerializeField] private Rigidbody _rigidbody;

    private SceneLoadHandler _sceneLoadHandler;
    private ICarLevel _carLevel;
    private ICarPositionSaver _positionSaver;

    private const string PositionKeyPrefix = "CarPosition";
    private const string RotationKeyPrefix = "CarRotation";

    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;

    [Inject]
    private void Construct(SceneLoadHandler sceneLoadHandler, ICarLevel carLevel, ICarPositionSaver positionSaver)
    {
        _sceneLoadHandler = sceneLoadHandler;
        _carLevel = carLevel;
        _positionSaver = positionSaver;
    }

    private void Awake()
    {
        // _rigidbody = GetComponent<Rigidbody>();
        _defaultPosition = Vector3.zero + new Vector3(0, _yOffset, 0);
        _defaultRotation = Quaternion.identity;
    }

    private void OnEnable()
    {
        _sceneLoadHandler.SceneLoaded += OnSceneLoaded;
        _sceneLoadHandler.SceneUnloaded += SavePosition;
    }

    private void Start()
    {
        SetPosition();
    }

    private void OnDisable()
    {
        SavePosition();

        _sceneLoadHandler.SceneLoaded -= OnSceneLoaded;
        _sceneLoadHandler.SceneUnloaded -= SavePosition;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        SavePosition();
    }

    private void OnSceneLoaded()
    {
        SetPosition();
    }

    /*  private string GenerateKey(string sceneName, string keyPrefix)  // to another class SpawnData
      {
          string divider = "|";

          return keyPrefix + divider + sceneName;
      }*/

    private void SetPosition()
    {
        /* string positionKey = GenerateKey(_sceneLoadHandler.SceneName, PositionKeyPrefix);
         string rotationKey = GenerateKey(_sceneLoadHandler.SceneName, RotationKeyPrefix);*/

        Vector3 yOffset = new Vector3(0, _yOffset, 0);
        Vector3 startPosition = _positionSaver.GetPosition(_carLevel.Value, _sceneLoadHandler.SceneName, _defaultPosition);
        startPosition += yOffset;

        Vector3 eulerRotation = _positionSaver.GetRotation(_carLevel.Value, _sceneLoadHandler.SceneName, _defaultRotation).eulerAngles;
        Quaternion startRotation = Quaternion.Euler(new Vector3(0, eulerRotation.y, 0));

        MessageBroker.Default.Publish(new CarStartSpawn
        {
            CarRigidbody = _rigidbody
        });

        _rigidbody.isKinematic = true;
        _rigidbody.transform.position = startPosition;
        _rigidbody.transform.rotation = startRotation;
        _rigidbody.isKinematic = false;

        MessageBroker.Default.Publish(new CarEndSpawn
        {
            CarRigidbody = _rigidbody
        });
    }

    private void SavePosition()
    {
        _positionSaver.SavePosition(_carLevel.Value, _sceneLoadHandler.SceneName, _rigidbody.transform.position);
        _positionSaver.SaveRotation(_carLevel.Value, _sceneLoadHandler.SceneName, _rigidbody.transform.rotation);
    }
}

public struct CarStartSpawn
{
    public Rigidbody CarRigidbody; // change to transform
}

public struct CarEndSpawn
{
    public Rigidbody CarRigidbody;
}
