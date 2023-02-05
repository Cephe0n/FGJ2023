using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameControl : MonoBehaviour
{

    public static bool WaveActive, GameOver;
    public int RemainingLumber, AxeHealth, AxeDamage, AxeWearRate, AxeRepairRate, RootsOnCrystal;
    public int FuelRestoredPerLumber;
    public float CrystalHealth, DarknessLevel;
    public Text LumberAmountText, FuelAmountText, UseHintText, UseErrorText, AxeHealthText, CrystalHealthText;

    [HideInInspector]
    public bool ErrorTextVisible;
    public Generator GeneratorScript;

    // Start is called before the first frame update
    void Start()
    {
        LumberAmountText.text = RemainingLumber.ToString();
        AxeHealthText.text = AxeHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        FuelAmountText.text = Mathf.Floor(GeneratorScript.FuelRemaining).ToString();
        CrystalHealthText.text = Mathf.Floor(CrystalHealth).ToString();

        DarknessLevel = (100 - GeneratorScript.FuelRemaining) / 100;

        if (RootsOnCrystal > 0)
        {
            CrystalHealth -= RootsOnCrystal * Time.deltaTime;
        }

        if (CrystalHealth <= 0)
        {
            CrystalHealth = 0;
           Lose();
        }
    }

    void Lose()
    {
        GameOver = true;
    }

    public void OnRestart()
    {
        if (GameOver)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DamageAxe()
    {
        AxeHealth -= AxeWearRate;

        if (AxeHealth < 0)
         AxeHealth = 0;

        AxeHealthText.text = AxeHealth.ToString();

        UpdateAxeDmg();
    }

    public void RepairAxe()
    {
        AxeHealth += AxeRepairRate;
        if (AxeHealth > 100)
         AxeHealth = 100;

         AxeHealthText.text = AxeHealth.ToString();

        UpdateAxeDmg();
    }

    void UpdateAxeDmg()
    {
        if (AxeHealth > 80)
        AxeDamage = 3;
        else if (AxeHealth > 40)
        AxeDamage = 2;
        else
        AxeDamage = 1;
    }

    public void GetLumber(int amount)
    {
        RemainingLumber += amount;
        LumberAmountText.text = RemainingLumber.ToString();
    }

    public void UsedLumber(int amount)
    {
        RemainingLumber -= amount;
        LumberAmountText.text = RemainingLumber.ToString();
    }

    public IEnumerator ErrorTextFade()
    {
        ErrorTextVisible = true;
        yield return new WaitForSecondsRealtime(2f);
        UseErrorText.text = "";
        ErrorTextVisible = false;

    }
}
