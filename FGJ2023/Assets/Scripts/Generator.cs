using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generator : Interactable
{

    public float FuelRemaining, FuelDrainRate;
    float FuelMax = 100;
    public GameControl GameControl;
    public GameObject[] lamps;

    // Start is called before the first frame update
    void Start()
    {
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
        if (GameControl.RemainingLumber > 0 && FuelRemaining < 100)
        {
            FuelRemaining += GameControl.FuelRestoredPerLumber;
            
            if (FuelRemaining > 100)
            FuelRemaining = 100;

            foreach(GameObject lamp in lamps)
            {
                var l = lamp.GetComponent<Light>();

                l.intensity = FuelRemaining / 100;

            }

            GameControl.UsedLumber(1);
        }
        
    }
}
