using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : EnemyScript
{
    public override void Die(bool fromwaveover = false) {
        EnemySpawner.StrongEnemiesCurr--;
        base.Die();
    }
}
