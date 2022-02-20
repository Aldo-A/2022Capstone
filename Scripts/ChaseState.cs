using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public bool isInRange;

    public override State RunCurrentState()
    {
        if (isInRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
