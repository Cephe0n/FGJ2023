using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyScript
{
    public override void Die()
    {
        EnemySpawner.BasicEnemiesCurr--;
        base.Die();
    }
}
