using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovableWheel 
{
    public void ForwardMove(float force);
    public void BackwardMove(float force);    
}
