using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockingScript agentPrefab;
    List<FlockingScript> agents = new List<FlockingScript>();
    public Flockbehaviour behaviour;
    NavPointSpawner navPointSpawner;
    public Transform formation;

    [Range(10, 500)]
    public int startingCount = 50;
    const float AgentDensity = 1f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(10f, 50f)]
    public float neighbourRadius = 50f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.1f;

    float squareMaxSpeed;
    float squareNeighbourRaidus;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRaidus = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRaidus * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockingScript newAgent = Instantiate(
                agentPrefab,
                new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)), 
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockingScript agent in agents)
        {
            //if (agent == agents[0])
            //{
            //    navPointSpawner = FindObjectOfType<NavPointSpawner>();
            //    formation = navPointSpawner.transform;
            //    Vector3 targetNav = formation.GetChild(0).position;
            //    agent.Move(targetNav);
            //}
            //else
            //{
            //    List<Transform> context = GetNearbyObjects(agent);
            //    //agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 50f);

            //    Vector3 move = behaviour.CalculateMove(agent, context, this);

            //    move *= driveFactor;
            //    if (move.sqrMagnitude > squareMaxSpeed)
            //    {
            //        move = move.normalized * maxSpeed;
            //    }
            //    agent.Move(move);
            //}

            List<Transform> context = GetNearbyObjects(agent);
            //agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 50f);

            Vector3 move = behaviour.CalculateMove(agent, context, this);

            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);

        }
    }

    List<Transform> GetNearbyObjects(FlockingScript agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        foreach(Collider c in contextColliders){
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
                
            }
        }
        return context;
    }
}
