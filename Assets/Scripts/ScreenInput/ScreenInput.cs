using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 _centrPoint;
    private bool _isPointerUnderPanel;
    private RectTransform targetRectTransform;
    private float _currentAngle;

    public event Action<float> AngleChanged;
    public event Action MouseUp;

    private void Awake()
    {
        _isPointerUnderPanel = false;
        targetRectTransform = GetComponent<RectTransform>();

        _centrPoint = targetRectTransform.localPosition;

        _currentAngle = float.MaxValue;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _isPointerUnderPanel = true;
        Debug.Log("Pointer under p[anel");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _isPointerUnderPanel = false;
         MouseUp?.Invoke();
    }

    void Update()
    {
        if (_isPointerUnderPanel)
        {
            if (Input.GetMouseButton(0))
            {

                Vector2 mousePos = Input.mousePosition;
                Vector2 localPoint;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    targetRectTransform.parent as RectTransform,
                    mousePos,
                    null,
                    out localPoint
                );

                Vector2 tapDirection = localPoint - _centrPoint;

                Vector2 upDirection = Vector2.up;

                float newAngle = CalculateAngle(upDirection, tapDirection, Vector3.back);

                if (newAngle != _currentAngle)
                {
                    _currentAngle = newAngle;
                    AngleChanged?.Invoke(newAngle);

                    Debug.Log("Control panel input angle changed");
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                MouseUp?.Invoke();
            }
        }
    }

    private float CalculateAngle(Vector3 vectorA, Vector3 vectorB, Vector3 normal) // same method in rotator
    {
        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }
}