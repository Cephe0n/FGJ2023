using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Waves : MonoBehaviour
{
    public int CurrentWave = 0;
    float TimeBetweenWaves, WaveLength;
    public Text WaveTimerText, WaveCountdownText, WaveCompleteText;
    public EnemySpawner Enemies;
    public Generator Generator;
    float currWaveTime, currCountdownTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextWave());
    }

    private void Update()
    {
        if (!GameControl.WaveActive)
        {
            WaveCountdownText.text = "Next wave in " + Mathf.Floor(currCountdownTime).ToString();
        }
        else
        {
            WaveTimerText.text = "WAVE " + CurrentWave + ": " + Mathf.Floor(currWaveTime).ToString() + "s";
        }
    }

    public IEnumerator NextWave()
    {

        UpdateWaveValues();
        currWaveTime = WaveLength;
        currCountdownTime = TimeBetweenWaves;
        WaveTimerText.enabled = false;
        WaveCountdownText.enabled = true;
        DOTween.To(()=> currCountdownTime, x=> currCountdownTime = x, 0, TimeBetweenWaves).SetEase(Ease.Linear);
        yield return new WaitForSecondsRealtime(TimeBetweenWaves);
        WaveTimerText.enabled = true;
        WaveCountdownText.enabled = false;
        GameControl.WaveActive = true;
        DOTween.To(()=> currWaveTime, x=> currWaveTime = x, 0, WaveLength).SetEase(Ease.Linear);
        yield return new WaitForSecondsRealtime(WaveLength);
        //KillAll();
        WaveTimerText.enabled = false;
        WaveCountdownText.enabled = false;
        WaveCompleteText.text = "Wave complete";
        GameControl.WaveActive = false;
        yield return new WaitForSecondsRealtime(3f);
        WaveCompleteText.text = "";
        yield return NextWave();

    }

/*     void KillAll()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            foreach (GameObject e in enemies)
            {
                if (e != null)
                e.GetComponent<EnemyScript>().Die();
            }
        }
    } */

    void UpdateWaveValues()
    {
        CurrentWave++;

        if (CurrentWave == 1)
            TimeBetweenWaves = 1f;
        else
        {
            TimeBetweenWaves = 5f;
        }

        switch (CurrentWave)
        {
            case 1:
                Enemies.EnemiesTotalMax = 3;
                Enemies.BasicEnemiesMax = 3;
                Enemies.FastEnemiesMax = 0;
                Enemies.StrongEnemiesMax = 0;
                WaveLength = 45f;
                break;

            case 2:
                Enemies.EnemiesTotalMax = 4;
                Enemies.BasicEnemiesMax = 3;
                Enemies.FastEnemiesMax = 1;
                Enemies.StrongEnemiesMax = 0;
                WaveLength = 50f;
                break;

            case 3:
                Enemies.EnemiesTotalMax = 5;
                Enemies.BasicEnemiesMax = 2;
                Enemies.FastEnemiesMax = 3;
                Enemies.StrongEnemiesMax = 0;
                WaveLength = 55f;
                break;

            case 4:
                Enemies.EnemiesTotalMax = 5;
                Enemies.BasicEnemiesMax = 2;
                Enemies.FastEnemiesMax = 2;
                Enemies.StrongEnemiesMax = 1;
                Generator.FuelDrainRate *= 1.2f;
                WaveLength = 60f;
                break;

            case 5:
                Enemies.EnemiesTotalMax = 6;
                Enemies.BasicEnemiesMax = 2;
                Enemies.FastEnemiesMax = 1;
                Enemies.StrongEnemiesMax = 3;
                WaveLength = 75f;
                break;

            case 6:
                Enemies.EnemiesTotalMax = 6;
                Enemies.BasicEnemiesMax = 1;
                Enemies.FastEnemiesMax = 3;
                Enemies.StrongEnemiesMax = 2;
                WaveLength = 90f;
                break;

            case 7:
                Enemies.EnemiesTotalMax = 7;
                Enemies.BasicEnemiesMax = 2;
                Enemies.FastEnemiesMax = 3;
                Enemies.StrongEnemiesMax = 2;
                Generator.FuelDrainRate *= 1.2f;
                WaveLength = 100f;
                break;

            case 8:
                Enemies.EnemiesTotalMax = 7;
                Enemies.BasicEnemiesMax = 0;
                Enemies.FastEnemiesMax = 4;
                Enemies.StrongEnemiesMax = 3;
                WaveLength = 120f;
                break;

            case 9:
                Enemies.EnemiesTotalMax = 8;
                Enemies.BasicEnemiesMax = 1;
                Enemies.FastEnemiesMax = 3;
                Enemies.StrongEnemiesMax = 4;
                Generator.FuelDrainRate *= 1.3f;
                WaveLength = 150f;
                break;

            case 10:
                Enemies.EnemiesTotalMax = 10;
                Enemies.BasicEnemiesMax = 2;
                Enemies.FastEnemiesMax = 3;
                Enemies.StrongEnemiesMax = 5;
                WaveLength = 240f;
                break;
        }
    }
}
