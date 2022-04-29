using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Boid/Behavior/Cohesion")]
public class CohesionBehavior : BoidBehavior
{
    
    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //if no neighbour, fly straight 
        if(context.Count == 0)
            return Vector2.zero;
        //add all point 

        Vector2 cohensionMove = Vector2.zero;
        foreach(Transform t in context)
        {
            cohensionMove += (Vector2) t.position;

        }
        cohensionMove /= context.Count;
        //offset of vector

        cohensionMove -=(Vector2)agent.transform.position;
        return cohensionMove;
         
    }

    // Start is called before the first frame update
    
}
