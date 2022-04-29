using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockingScript agent, List<Transform> context, Flock flock)
    {
        //if no neighbours maintain current alignment
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }
        //add all points together and avg
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.transform.forward;
        }
        alignmentMove /= context.Count;
        alignmentMove.y = 0;
        return alignmentMove;
    }
}
