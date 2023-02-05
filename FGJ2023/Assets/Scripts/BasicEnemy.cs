using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyScript
{

    int MaxHealth = 5; 

    protected override void OnEnable()
    {
        Health = MaxHealth; 
        base.OnEnable();
    }
    public override void Die()
    {
        EnemySpawner.BasicEnemiesCurr--;
        base.Die();
    }
}
