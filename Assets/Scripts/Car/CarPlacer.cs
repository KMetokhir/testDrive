using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;
using UniRx;

public class CarPlacer : MonoBehaviour
{
    [SerializeField] private SceneLoadHandler _sceneLoadHandler; // added in CarPrefab but it allready excicts in AtrractablesSpaen System prefab use zenject
    [SerializeField] private float _yOffset;

    private const string PositionKeyPrefix = "CarPosition";
    private const string RotationKeyPrefix = "CarRotation";
    private Rigidbody _rigidbody;

    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _defaultPosition = Vector3.up;
        _defaultRotation = Quaternion.identity;

        SetPosition();
    }

    private void OnEnable()
    {
        _sceneLoadHandler.SceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded()
    {
        SetPosition();
    }

    private void OnDisable()
    {
        SavePosition();

        _sceneLoadHandler.SceneLoaded -= OnSceneLoaded;
    }

    private string GenerateKey(string sceneName, string keyPrefix)
    {
        string divider = "|";

        return keyPrefix + divider + sceneName;
    }

    private void SetPosition()
    {
        string positionKey = GenerateKey(_sceneLoadHandler.SceneName, PositionKeyPrefix);
        string rotationKey = GenerateKey(_sceneLoadHandler.SceneName, RotationKeyPrefix);

        Vector3 yOffset = new Vector3(0, _yOffset, 0);
        Vector3 startPosition = PlayerPrefsManager.GetVector3(positionKey, _defaultPosition);
        startPosition += yOffset;

        Vector3 eulerRotation = PlayerPrefsManager.GetVector3(rotationKey, _defaultRotation.eulerAngles);
        Quaternion startRotation = Quaternion.Euler(new Vector3(0, eulerRotation.y, 0));

        _rigidbody.isKinematic = true;

        _rigidbody.transform.position = startPosition;
        _rigidbody.transform.rotation = startRotation;


        //_rigidbody.isKinematic = false;

        Observable.NextFrame(FrameCountType.FixedUpdate)
           .Subscribe(_ =>
           {
               _rigidbody.isKinematic = false;

               MessageBroker.Default.Publish(new CarSpawned
               {
                   CarRigidbody = _rigidbody
               });
           })
           .AddTo(this);
    }

    private void SavePosition()
    {
        PlayerPrefsManager.SaveVector3(GenerateKey(_sceneLoadHandler.SceneName, PositionKeyPrefix), _rigidbody.transform.position);
        PlayerPrefsManager.SaveVector3(GenerateKey(_sceneLoadHandler.SceneName, RotationKeyPrefix), _rigidbody.transform.rotation.eulerAngles);
    }
}

public struct CarSpawned
{
    public Rigidbody CarRigidbody;
}

public struct CarStartSpawn
{
    public Rigidbody CarRigidbody;
}

public struct CarEndSpawn
{
    public Rigidbody CarRigidbody;
}

