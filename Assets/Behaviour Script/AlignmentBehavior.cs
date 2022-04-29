using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Boid/Behavior/Alignment")]
public class AlignmentBehavior : BoidBehavior
{
    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //if no neighbour, fly straight 
        if (context.Count == 0)
            return agent.transform.up;
        //add all point 

        //
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform t in context)
        {
            alignmentMove += (Vector2)t.transform.up;

        }
        alignmentMove /= context.Count;
        //offset of vector

        //alignmentMove -= (Vector2)agent.transform.position;
        return alignmentMove;

    }
}

 

