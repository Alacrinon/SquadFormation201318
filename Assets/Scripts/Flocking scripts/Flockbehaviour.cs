using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flockbehaviour : ScriptableObject
{
    public abstract Vector3 CalculateMove(FlockingScript agent, List<Transform> context, Flock flock);
}
