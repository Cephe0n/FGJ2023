using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generator : Interactable
{

    public float FuelRemaining, FuelDrainRate;
    public int LumberNeeded;
    float FuelMax = 100;
    public GameObject[] lamps;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UseText = "Add Fuel (E)";
        lamps = GameObject.FindGameObjectsWithTag("Lamp");
    }

    void Update()
    {
        if (FuelRemaining >= 0 && GameControl.WaveActive)
        {

        FuelRemaining -= FuelDrainRate * Time.deltaTime;

        if (FuelRemaining < 0)
        FuelRemaining = 0;

        foreach(GameObject lamp in lamps)
        {
            var l = lamp.GetComponent<Light>();

            l.intensity -= (FuelDrainRate / 100) * Time.deltaTime;
        }

        }
    }

    public override void Use()
    {

        if (GameControl.RemainingLumber < LumberNeeded)
        {
            UseErrorText = "Puu puuttuu :D";
        }
        else if (FuelRemaining >= FuelMax - 10)
        {
            UseErrorText = "Ei mahu enempää :D";
        }



        if (GameControl.RemainingLumber > 0 && FuelRemaining < FuelMax)
        {
            FuelRemaining += GameControl.FuelRestoredPerLumber;
            
            if (FuelRemaining > FuelMax)
            FuelRemaining = FuelMax;

            foreach(GameObject lamp in lamps)
            {
                var l = lamp.GetComponent<Light>();

                l.intensity = FuelRemaining / FuelMax;

            }

            GameControl.UsedLumber(1);
        }
        else 
        {
            
            GameControl.UseErrorText.text = UseErrorText;

            if (!GameControl.ErrorTextVisible)
                StartCoroutine(GameControl.ErrorTextFade());
        }
        
    }

}
