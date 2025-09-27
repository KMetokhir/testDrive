using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    [SerializeField] private Vector2 _centrPoint;

    private bool _isPointerUnderPanel;
    public RectTransform targetRectTransform;

    public event Action<float> AngleChanged;
    public event Action MouseUp;

    private float _angle;


    private void Awake()
    {
        _isPointerUnderPanel = false;
        targetRectTransform = GetComponent<RectTransform>();

        _centrPoint = targetRectTransform.localPosition;

        _angle = float.MaxValue;
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
      //  Debug.Log("Cursor Entering " + name + " GameObject");

        _isPointerUnderPanel = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
       // Debug.Log("Cursor Exiting " + name + " GameObject");

        _isPointerUnderPanel = false;
        MouseUp?.Invoke();
    }

   

    void Update()
    {
        if (_isPointerUnderPanel)
        {
            if (Input.GetMouseButton(0))
            {
                /*var mousePosition = Input.mousePosition;

                float x = mousePosition.x;
                float y =  mousePosition.y;
                
                *//*RectTransform picture = GetComponent<RectTransform>();
                picture.anchoredPosition = new Vector2(x, y);*//*

                Debug.Log($"{x} - {y}");*/

                Vector2 mousePos = Input.mousePosition;
                Vector2 localPoint;

                // Convert screen point to local point in the parent RectTransform
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    targetRectTransform.parent as RectTransform,
                    mousePos,
                    null,
                    out localPoint
                );

                // Set the RectTransform's local position
               // targetRectTransform.localPosition = localPoint;

                Vector2 tapDirection= localPoint - _centrPoint;


                Vector2 upDirection =  Vector2.up;

                float newAngle = CalculateAngle(upDirection, tapDirection);

                if(newAngle != _angle)
                {
                    _angle = newAngle;
                    AngleChanged?.Invoke(newAngle);
                }

               // Debug.Log(CalculateAngle(upDirection, tapDirection));

            }

            if (Input.GetMouseButtonUp(0))
            {
                MouseUp?.Invoke();
            }
        }
    }

    private float CalculateAngle(Vector3 vectorA, Vector3 vectorB)
    {
        /*float z = 0;
        vectorA = new Vector3(vectorA.x, vectorA.y, z);
        vectorB = new Vector3(vectorB.x, vectorB.y, z);*/

        float sign = Mathf.Sign(Vector3.Dot(Vector3.back, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }
}
