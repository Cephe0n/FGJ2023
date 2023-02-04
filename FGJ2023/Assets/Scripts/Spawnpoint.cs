using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public bool HasEnemy, OnCooldown;
    public float SpawnCooldown;
    Transform enemyChecker;

    void Start()
    {
        enemyChecker = this.transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        
        if (HasEnemy)
        {
            if (!Physics.Raycast(enemyChecker.position, Vector3.down, 200f, LayerMask.GetMask("Attackable")))
            {
                HasEnemy = false;

                if (!OnCooldown)
                StartCoroutine(SpawnCd());
            }
        }

    }

    private IEnumerator SpawnCd()
    {
        OnCooldown = true;
        yield return new WaitForSecondsRealtime(SpawnCooldown);
        OnCooldown = false;
    }

}
