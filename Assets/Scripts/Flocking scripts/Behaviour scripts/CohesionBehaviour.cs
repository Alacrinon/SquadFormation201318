using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockingScript agent, List<Transform> context, Flock flock)
    {
        //if no neighbours return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }
        //add all points together and avg
        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count;
        //create offset from agent position
        cohesionMove -= agent.transform.position;
        cohesionMove.y = 0;
        return cohesionMove;
    }

 
}
