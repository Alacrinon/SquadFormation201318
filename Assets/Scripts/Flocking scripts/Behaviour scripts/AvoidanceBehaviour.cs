using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockingScript agent, List<Transform> context, Flock flock)
    {
        //if no neighbours return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }
        //add all points together and avg
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid += 1;
                avoidanceMove += agent.transform.position - item.position;
            }
            
        }
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }
        avoidanceMove.y = 0;
        return avoidanceMove;
    }
}
