using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    [SerializeField] private ScreenInput _screenInput;

    [SerializeField] private RightRotaryWheel _rightRotaryWheel;
    [SerializeField] private LeftRotaryWheel _leftRotaryWheel;

    [SerializeField] private List<RotaryWheel> _rotaryWheels;
    [SerializeField] private List<DrivingWheel> _drivingWheels;

    [SerializeField] private float _moveForce;


    private void OnEnable()
    {
        _screenInput.AngleChanged += OnAngleChanged;
        _screenInput.MouseUp += OnMouseEventUp;
    }

    private void OnMouseEventUp()
    {
        //throw new NotImplementedException();
    }

    private void OnAngleChanged(float angle)
    {
        if(Mathf.Abs(angle) <= 90)
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.ForwardMove(_moveForce);

            }
        }
        else
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.BackwardMove(_moveForce);

            }

        }
    }

    private void Start()
    {
       /* _drivingWheels = new List<DrivingWheel>();
        _drivingWheels.Add(_leftRotaryWheel);
        _drivingWheels.Add(_rightRotaryWheel);*/
    }

    private void Update()
    {
       /* if (Input.GetKeyDown("w"))
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.ForwardMove(_moveForce);

            }
        }

        if (Input.GetKeyUp("w") && Input.GetKey("s") == false)
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.StopMoving();

            }
        }

        if (Input.GetKeyDown("s"))
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.BackwardMove(_moveForce);

            }
        }

        if (Input.GetKeyUp("s") && Input.GetKey("w") == false)
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.StopMoving();
            }
        }*/

        if (Input.GetKeyDown("d"))
        {
            /* _rightRotaryWheel.RotateWheel(30);
             _leftRotaryWheel.RotateWheel(30);*/

            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.RotateWheel(30);
            }
        }

        if (Input.GetKeyUp("d") && Input.GetKey("a") == false)
        {
            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.StopRotation();
            }
        }


        if (Input.GetKeyDown("a"))
        {
            /*_rightRotaryWheel.RotateWheel(-30);
            _leftRotaryWheel.RotateWheel(-30);*/
            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.RotateWheel(-30);
            }
        }

        if (Input.GetKeyUp("a") && Input.GetKey("d") == false)
        {
            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.StopRotation();
            }
        }
    }
}
