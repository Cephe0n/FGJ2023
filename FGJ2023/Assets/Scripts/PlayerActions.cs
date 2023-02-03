using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    public AxeActions AxeScript;
    public Interactable i;

    public void OnAttack()
    {
        AxeScript.Attack();
    }

    public void OnUse()
    {
        i.Use();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
