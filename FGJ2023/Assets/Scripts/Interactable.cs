using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string UseText, UseErrorText;

    protected GameControl GameControl;

    protected virtual void Start() 
    {
        GameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
    }

    public virtual void Use()
    {

    }
}
