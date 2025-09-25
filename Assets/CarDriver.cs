using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    [SerializeField] private RightRotaryWheel _rightRotaryWheel;
    [SerializeField] private LeftRotaryWheel _leftRotaryWheel;

    [SerializeField] private List<RotaryWheel> _rotaryWheels;

    [SerializeField] private float _moveForce;
    private List<DrivingWheel> _drivingWheels;

    private void Start()
    {
        _drivingWheels = new List<DrivingWheel>();
        _drivingWheels.Add(_leftRotaryWheel);
        _drivingWheels.Add(_rightRotaryWheel);
    }

    private void Update()
    {
        if (Input.GetKey("w"))
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.ForwardMove(_moveForce);

            }
        }

        if (Input.GetKey("s"))
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.BackwardMove(_moveForce);

            }
        }       

   

        if (Input.GetKeyDown("d"))
        {
           /* _rightRotaryWheel.RotateWheel(30);
            _leftRotaryWheel.RotateWheel(30);*/

            foreach(RotaryWheel wheel in _rotaryWheels)
            {
                wheel.RotateWheel(30);
            }
        }

        if (Input.GetKeyUp("d"))
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

        if (Input.GetKeyUp("a"))
        {
            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.StopRotation();
            }
        }
    }
}
