using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 _centrPoint;
    private bool _isPointerUnderPanel;
    private RectTransform _targetRectTransform;
    private float _currentAngle;

    public bool IsDrivButtonPressed { get; private set; }

    public event Action<float> AngleChanged;
    public event Action MouseUp;

    private void Awake()
    {
        IsDrivButtonPressed = false;

        _isPointerUnderPanel = false;
        _targetRectTransform = GetComponent<RectTransform>();

        _centrPoint = _targetRectTransform.localPosition;

        _currentAngle = float.MaxValue;
    }

    private void Update()
    {
        if (_isPointerUnderPanel)
        {
            if (Input.GetMouseButton(0))
            {
                IsDrivButtonPressed = true;

                ProcessAngle();
            }
        }
        else if (IsDrivButtonPressed)
        {
            ProcessAngle();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseUp?.Invoke();
            IsDrivButtonPressed = false;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _isPointerUnderPanel = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _isPointerUnderPanel = false;
    }

    private void ProcessAngle()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _targetRectTransform.parent as RectTransform,
            mousePos,
            null,
            out localPoint
        );

        Vector2 tapDirection = localPoint - _centrPoint;

        Vector2 upDirection = Vector2.up;

        float newAngle = AngleOnPlaneCalculator.CalculateAngle(upDirection, tapDirection, Vector3.back);

        if (newAngle != _currentAngle)
        {
            _currentAngle = newAngle;
            AngleChanged?.Invoke(newAngle);
        }
    }
}