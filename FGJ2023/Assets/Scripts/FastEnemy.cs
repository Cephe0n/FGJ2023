using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : EnemyScript
{
    int MaxHealth = 2; 

    protected override void OnEnable()
    {
        Health = MaxHealth; 
        base.OnEnable();
    }
    public override void Die() 
    {
        EnemySpawner.FastEnemiesCurr--;
        base.Die();
    }
}
