using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MagnetSetingsProvider : CarComponentProvider<IMagnetData> , IMagnetData
{
    public float MagnetRadius => Component.MagnetRadius;

    public MagnetSetingsProvider(SignalBus signalBus) : base(signalBus) { }
   
}
