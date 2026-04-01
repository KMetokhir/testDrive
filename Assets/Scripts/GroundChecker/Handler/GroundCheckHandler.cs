using System.Collections.Generic;
using UnityEngine;

public class GroundCheckHandler : MonoBehaviour, IGroundCheckHandler
{
    private IWheelsHandler _wheelHandler;
    private IReadOnlyList<IGroundChecker> GroundCheckers => _wheelHandler?.Wheels;

    private void Awake()
    {
        _wheelHandler = GetComponentInChildren<IWheelsHandler>();
    }

    public bool IsGrounded()
    {
        foreach (IGroundChecker groundChecker in GroundCheckers)
        {
            if (groundChecker.IsGrounded == false)
            {
                return false;
            }
        }

        return true;
    }
}
