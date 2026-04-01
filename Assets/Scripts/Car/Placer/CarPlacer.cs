using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

public class CarPlacer : MonoBehaviour
{
    [SerializeField] private float _yOffset;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int _saveInterval = 10;

    private Vector3 _lastSavedPosition;
    private Quaternion _lastSavedRotation;

    private Coroutine _saveCoroutine;
    private bool _isWorking;

    private SceneLoadHandler _sceneLoadHandler;
    private ICarLevel _carLevel;
    private ICarPositionSaver _positionSaver;

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
        _defaultPosition = Vector3.zero + new Vector3(0, _yOffset, 0);
        _defaultRotation = Quaternion.identity;

        MessageBroker.Default
            .Receive<CarStuckEvent>()
            .TakeUntilDestroy(this)
            .Subscribe(OnCarStuck);
    }

    private void OnCarStuck(CarStuckEvent @event)
    {
        SetPosition();
    }

    private void OnEnable()
    {
        _sceneLoadHandler.SceneLoaded += OnSceneLoaded;
        _sceneLoadHandler.SceneUnloaded += SavePosition;

        _isWorking = true;

        _saveCoroutine = StartCoroutine(SavePositionCoroutine(_saveInterval));
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

        _isWorking = false;

        if (_saveCoroutine != null)
        {
            StopCoroutine(_saveCoroutine);
            _saveCoroutine = null;
        }
    }

    private void OnSceneLoaded()
    {
        SetPosition();
    }

    private IEnumerator SavePositionCoroutine(int waitingTime)
    {
        WaitForSeconds wait = new WaitForSeconds(waitingTime);

        while (_isWorking)
        {
            yield return wait;
            SavePosition();
        }
    }

    private void SetPosition()
    {
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
        if (_rigidbody.transform.position == _lastSavedPosition && _rigidbody.transform.rotation == _lastSavedRotation)
        {
            return;
        }

        _lastSavedPosition = _rigidbody.transform.position;
        _lastSavedRotation = _rigidbody.transform.rotation;

        _positionSaver.SavePosition(_carLevel.Value, _sceneLoadHandler.SceneName, _lastSavedPosition);
        _positionSaver.SaveRotation(_carLevel.Value, _sceneLoadHandler.SceneName, _lastSavedRotation);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        SavePosition();
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