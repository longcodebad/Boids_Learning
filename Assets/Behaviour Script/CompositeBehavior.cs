using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Boid/Behavior/Composite")]
public class CompositeBehavior : BoidBehavior
{

    public BoidBehavior[] behaviors;
    public float[] weights;
    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        //for mismatch data
        if (weights.Length != behaviors.Length)
        {
            Debug.Log("Data Mismatch in "+ name, this);
            return Vector2.zero;

        }

        //setup move
        Vector2 move = Vector2.zero;
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, boid)*weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *=weights[i];
                }
                move +=partialMove;
            }
        }
        return move;

    }

}
