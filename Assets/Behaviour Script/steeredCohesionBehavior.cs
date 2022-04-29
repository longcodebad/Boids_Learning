using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Steered Cohesion")]
public class steeredCohesionBehavior : BoidBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    // Start is called before the first frame update
    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //if no neighbour, fly straight 
        if (context.Count == 0)
            return Vector2.zero;
        //add all point 

        Vector2 cohensionMove = Vector2.zero;
        foreach (Transform t in context)
        {
            cohensionMove += (Vector2)t.position;

        }
        cohensionMove /= context.Count;
        //offset of vector

        cohensionMove -= (Vector2)agent.transform.position;
        cohensionMove = Vector2.SmoothDamp(agent.transform.up, cohensionMove,ref currentVelocity,agentSmoothTime);
        return cohensionMove;

    }
}
