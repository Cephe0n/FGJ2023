using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : EnemyScript
{
    public override void Die() 
    {
        EnemySpawner.FastEnemiesCurr--;
        base.Die();
    }
}
