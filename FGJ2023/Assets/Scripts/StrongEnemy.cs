using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : EnemyScript
{

    int MaxHealth = 8; 

    protected override void OnEnable()
    {
        Health = MaxHealth; 
        base.OnEnable();
    }
    public override void Die() {
        EnemySpawner.StrongEnemiesCurr--;
        base.Die();
    }
}
