
using UnityEngine;

public static class InputDataCorrector 
{
   public static float Correct(float inputNumber,float maxInput, float maxAngle  )
    {
        maxInput = Mathf.Abs(maxInput);
        maxAngle = Mathf.Abs(maxAngle);
        return inputNumber = (maxAngle / maxInput) * inputNumber;
    }
}
