using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static GameObject[] SpawnPoints;
    public GameObject EnemyFast, EnemyBasic, EnemyStrong;
    public int EnemiesTotalMax, BasicEnemiesMax, FastEnemiesMax, StrongEnemiesMax;
    public float SpawnDelay;
    public static int EnemiesTotalCurr, BasicEnemiesCurr, FastEnemiesCurr, StrongEnemiesCurr;
    bool spawnCd;


    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
    }   

    void Update()
    {
        if (!spawnCd && GameControl.WaveActive && EnemiesTotalCurr < EnemiesTotalMax)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        spawnCd = true;
        bool spawnBasic = BasicEnemiesCurr < BasicEnemiesMax ? true : false;
        bool spawnFast = FastEnemiesCurr < FastEnemiesMax ? true : false;
        bool spawnStrong = StrongEnemiesCurr < StrongEnemiesMax ? true : false;
        int spawnpointnum1 = (int)Random.Range(0, SpawnPoints.Length);
        int spawnpointnum2 = (int)Random.Range(0, SpawnPoints.Length);
        int spawnpointnum3 = (int)Random.Range(0, SpawnPoints.Length);

        yield return new WaitForSecondsRealtime(SpawnDelay);

        if (spawnBasic)
        {
            Spawn(EnemyBasic, 1, spawnpointnum1);
        }

        if (spawnFast)
        {
            Spawn(EnemyFast, 2, spawnpointnum2);
        }

        if (spawnStrong)
        {
            Spawn(EnemyStrong, 3, spawnpointnum3);
        }

        spawnCd = false;
    }

    void Spawn(GameObject pEnemy, int pType, int spawnpointnum)
    {
            
            GameObject spawnpoint = null;

            if (!SpawnPoints[spawnpointnum].GetComponent<Spawnpoint>().HasEnemy)
            {
                spawnpoint = SpawnPoints[spawnpointnum];
            }
            else
            {
                for (int i = 0; i < SpawnPoints.Length; i++)
                {
                    if (!SpawnPoints[i].GetComponent<Spawnpoint>().HasEnemy)
                    {
                        spawnpoint = SpawnPoints[i];
                        break;
                    }

                }
            }

            if (spawnpoint != null && !spawnpoint.GetComponent<Spawnpoint>().HasEnemy && !spawnpoint.GetComponent<Spawnpoint>().OnCooldown)
            {
                spawnpoint.GetComponent<Spawnpoint>().HasEnemy = true;
                EnemiesTotalCurr++;

                switch (pType)
                {
                    case 1:
                    BasicEnemiesCurr++;
                    break;
                    case 2:
                    FastEnemiesCurr++;
                    break;
                    case 3:
                    StrongEnemiesCurr++;
                    break;
                }

                ObjectPooler.Generate(pEnemy, spawnpoint.transform.position, spawnpoint.transform.rotation);
            }
      
    }


}
