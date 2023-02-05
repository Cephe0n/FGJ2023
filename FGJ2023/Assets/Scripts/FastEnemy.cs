using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : EnemyScript
{
    public override void Die(bool fromwaveover = false) 
    {
        EnemySpawner.FastEnemiesCurr--;
        base.Die();
    }
}
