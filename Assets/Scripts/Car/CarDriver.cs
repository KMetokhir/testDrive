using System;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    [SerializeField] private ScreenInput _screenInput;
    [SerializeField] private Speed _speed; //ISpeed
    [SerializeField] private Rotation _rotation;

    [SerializeField] private WheelBaseSpawner _wheelBaseSpawner;

    [SerializeField] private List<RotaryWheel> _rotaryWheels;
    [SerializeField] private List<DrivingWheel> _drivingWheels;

    [SerializeField] private float _moveForce;

    private void Awake()
    {
        _rotaryWheels = new List<RotaryWheel>();
        _drivingWheels = new List<DrivingWheel>();
    }
    private void OnEnable()
    {
        _wheelBaseSpawner.PartSpawned += SubcribeToBase;

    }

    private void OnDisable()
    {
        _wheelBaseSpawner.PartSpawned -= SubcribeToBase;
        UnsubscribeInput();
    }

    private void SubscribeInput()
    {
        _screenInput.AngleChanged += OnAngleChanged;
        _screenInput.MouseUp += OnMouseEventUp;

    }

    private void UnsubscribeInput()
    {
        _screenInput.AngleChanged -= OnAngleChanged;
        _screenInput.MouseUp -= OnMouseEventUp;

    }

    private void SubcribeToBase(WheelBase wheelBase)
    {
        wheelBase.WheelSpawned += SetWheel;
        wheelBase.WheelDestroied += RemoveWheel;
        wheelBase.Destroied += OnBaseDestroied; // destroyObject method avalible

        SubscribeInput();
    }

    private void UnsubscribeFrombase(WheelBase wheelBase)
    {
        wheelBase.WheelSpawned -= SetWheel;
        wheelBase.WheelDestroied -= RemoveWheel;
        wheelBase.Destroied -= OnBaseDestroied;

    }

    private void RemoveWheel(IWheel wheel)
    {

        if (wheel is DrivingWheel)
        {
            _drivingWheels.Remove(wheel as DrivingWheel);
        }

        if (wheel is RotaryWheel)
        {
            _rotaryWheels.Remove(wheel as RotaryWheel);
        }
    }

    private void OnBaseDestroied(UpgradePart part)
    {

        UnsubscribeInput();

        WheelBase wheelBase = (WheelBase)part;

        if (wheelBase == null)
        {
            throw new Exception("Upgrade  not WheelBase type");
        }

        UnsubscribeFrombase(wheelBase);
        _drivingWheels.Clear();
        _rotaryWheels.Clear();
    }

    private void SetWheel(IWheel wheel)
    {

        if (wheel is DrivingWheel)
        {
            _drivingWheels.Add(wheel as DrivingWheel);

        }

        if (wheel is RotaryWheel)
        {
            _rotaryWheels.Add(wheel as RotaryWheel);


        }
    }

    private void OnMouseEventUp()
    {
        foreach (DrivingWheel wheel in _drivingWheels)
        {
            wheel.StopMoving();

        }

        foreach (RotaryWheel wheel in _rotaryWheels)
        {
            wheel.StopRotation();
        }
    }

    private void OnAngleChanged(float angle)
    {
        float maxInputAngle = 90;


        if (Mathf.Abs(angle) <= maxInputAngle)
        {
            angle = InputDataCorrector.Correct(angle, maxInputAngle, _rotation.MaxAngle);

            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.ForwardMove(_speed);

            }

            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.RotateWheel(angle, _rotation);
            }
        }
        else
        {
            float sign = Mathf.Sign(angle);
            angle = sign * (180 - Mathf.Abs(angle));

            angle = InputDataCorrector.Correct(angle, maxInputAngle, _rotation.MaxAngle);

            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.BackwardMove(_speed);

            }

            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.RotateWheel(angle, _rotation);
            }
        }
    }

    private void Update()
    {

        #region "TestInput"
        if (Input.GetKeyDown("w"))
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.ForwardMove(_speed);

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
                wheel.BackwardMove(_speed);

            }
        }

        if (Input.GetKeyUp("s") && Input.GetKey("w") == false)
        {
            foreach (DrivingWheel wheel in _drivingWheels)
            {
                wheel.StopMoving();
            }
        }

        if (Input.GetKeyDown("d"))
        {
            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.RotateWheel(90, _rotation);
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
            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.RotateWheel(-90, _rotation);
            }
        }

        if (Input.GetKeyUp("a") && Input.GetKey("d") == false)
        {
            foreach (RotaryWheel wheel in _rotaryWheels)
            {
                wheel.StopRotation();
            }
        }
        #endregion

    }
}