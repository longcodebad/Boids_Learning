using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Advoidance")]
public class AdvoidanceBehavior : BoidBehavior
{
   //if no neighbour, fly straight 
        

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
       if(context.Count == 0)
            return Vector2.zero;
       
        //add all point 

        Vector2 advoidanceMove = Vector2.zero;
        int nAdvoid = 0;
        foreach(Transform t in context)
        {
            if(Vector2.SqrMagnitude(t.position - agent.transform.position) < boid.SquareAdvoidanceRadius) 
            { 
                nAdvoid++;
                //when advoid flick toward 
                advoidanceMove += (Vector2) (agent.transform.position-t.position);
            }
        }
        if(nAdvoid > 0)
        {
            advoidanceMove /= nAdvoid;
        }
        return advoidanceMove;
    }

         
}
