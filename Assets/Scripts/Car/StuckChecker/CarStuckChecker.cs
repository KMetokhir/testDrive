using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

public class CarStuckChecker : MonoBehaviour
{
    [SerializeField] private float _minVelocity = 0.2f;
    [SerializeField] private float _stuckTime = 2f;

    private ScreenInput _screenInput;
    private ICarBody _carBody;

    private Coroutine _stuckCoroutine;

    [Inject]
    private void Construct(ScreenInput screenInput, ICarBody carBody)
    {
        _screenInput = screenInput;
        _carBody = carBody;
    }

    private void OnDisable()
    {
        StopCheck();
    }

    private void Update()
    {
        if (_screenInput.IsDrivButtonPressed)
        {
            if (_carBody.Rigidbody.velocity.magnitude < _minVelocity)
            {
                if (_stuckCoroutine == null)
                    _stuckCoroutine = StartCoroutine(CheckIfStuck());
            }
            else
            {
                StopCheck();
            }
        }
        else
        {
            StopCheck();
        }
    }

    private IEnumerator CheckIfStuck()
    {
        float timer = 0f;

        while (timer < _stuckTime)
        {
            if (!_screenInput.IsDrivButtonPressed && _carBody.Rigidbody.velocity.magnitude >= _minVelocity)
            {
                _stuckCoroutine = null;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        MessageBroker.Default.Publish(new CarStuckEvent());

        _stuckCoroutine = null;
    }

    private void StopCheck()
    {
        if (_stuckCoroutine != null)
        {
            StopCoroutine(_stuckCoroutine);
            _stuckCoroutine = null;
        }
    }
}

