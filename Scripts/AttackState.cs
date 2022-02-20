using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("ATTACKED");
        return this;
    }
}

