using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadScript : MonoBehaviour
{
    SquadScript squadParent;
    FormationScript formationScript;
    public PathfinderScript agentPrefab;
    List<PathfinderScript> agents = new List<PathfinderScript>();
    public int agentCount = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSquad()
    {
        for (int i = 0; i < agentCount; i++)
        {
            PathfinderScript newAgent = Instantiate(
                agentPrefab,
                ((new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * agentCount * 2) + Vector3.up),
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    public void FormSquad()
    {
        foreach (PathfinderScript child in agents)
        {
            child.atLocation = false; //Will just need to move navpoints once movement of the points is implemented
        }

    }

    public void AssignToPoints()
    {
        formationScript = FindObjectOfType<FormationScript>();
        PathfinderScript[] pathfinders;
        pathfinders = FindObjectsOfType<PathfinderScript>();
        for (int i = 0; i < agents.Count; i++)
        {
            Debug.Log(i);
            pathfinders[i].ChosenNav = i;
        }
            
        
    }
}
