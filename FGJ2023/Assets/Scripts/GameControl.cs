using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public bool WaveActive = true;
    public int RemainingLumber, FuelRestoredPerLumber;
    public Text LumberAmountText, FuelAmountText;

    public Generator GeneratorScript;
    // Start is called before the first frame update
    void Start()
    {
        LumberAmountText.text = RemainingLumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        FuelAmountText.text = Mathf.Floor(GeneratorScript.FuelRemaining).ToString();
    }

    public void UsedLumber(int amount)
    {
        RemainingLumber -= amount;
        LumberAmountText.text = RemainingLumber.ToString();
    }
}
