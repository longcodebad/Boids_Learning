using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour

{
    // Start is called before the first frame update
    public BoidAgent agentPrefab;
    List<BoidAgent> agents = new List<BoidAgent>(); 
    public BoidBehavior behavior;

    [Range(10, 500)] public int startingCount = 250;
   
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(0.1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float advoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAdvoidanceRadius;
    public float SquareAdvoidanceRadius { get { return squareAdvoidanceRadius; } }
    
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAdvoidanceRadius = squareNeighborRadius * advoidanceRadiusMultiplier*advoidanceRadiusMultiplier ;
    
        for(int i = 0; i < startingCount; i++)
        {
            BoidAgent newAgent =Instantiate(
                agentPrefab,
                Random.insideUnitCircle*startingCount*AgentDensity,
                Quaternion.Euler(Vector3.forward*Random.Range(0f,360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            Color32 col = Random.Range(0, 2) == 1 ? new Color32(255, 95 , 109, 255) : new Color32(255 , 195 , 55 , 255);
            newAgent.GetComponentInChildren<SpriteRenderer>().color = col;
                
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(BoidAgent agent in agents)
        {
            List<Transform> context = getNearbyObject(agent);
           
            //demo
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white,Color.red,context.Count/6f);
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move = move * driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;

            }
            agent.Move(move);
        }
    }

    List<Transform> getNearbyObject(BoidAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextCollider = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextCollider)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }return context;
    }

}
