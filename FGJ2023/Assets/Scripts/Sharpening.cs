using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharpening : Interactable
{
    public float UseCooldown;
    bool OnCd;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UseText = "Sharpen Axe (E)";
        UseErrorText = "Can't sharpen more!";
    }

    public override void Use()
    {
        if (!OnCd && GameControl.AxeHealth < 100)
        {
            GameControl.RepairAxe();
            StartCoroutine(SharpenCd());
        }
    }

    IEnumerator SharpenCd()
    {
        OnCd = true;
        UseText = "";
        yield return new WaitForSecondsRealtime(UseCooldown);
        UseText = "Sharpen Axe (E)";
        OnCd = false;
    }
}
