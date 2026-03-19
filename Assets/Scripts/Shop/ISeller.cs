using System.Collections.Generic;
using UnityEngine;

public interface ISeller
{
    public Transform SellerTransform { get; }
    public List<IAttractable> Buy();
}