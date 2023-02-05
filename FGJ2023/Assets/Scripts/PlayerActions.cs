using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{

    public AxeActions AxeScript;
    public Interactable i;
    Ray useRay;
    RaycastHit hit;
    public GameControl GameControl;
    GameObject CurrentlyUsing;

    public void OnAttack()
    {
        AxeScript.Attack();
    }

    public void OnUse()
    {
        if (CurrentlyUsing != null)
        {
            var i = CurrentlyUsing.GetComponent<Interactable>();
            i.Use();
        }
    }

        public void OnRestart()
    {
        if (GameControl.GameOver)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        useRay = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        if (Physics.Raycast(useRay, out hit, 1.5f, LayerMask.GetMask("Interactable")))
        {
            if (CurrentlyUsing == null)
            {
            CurrentlyUsing = hit.collider.gameObject;
            }
            GameControl.UseHintText.text = CurrentlyUsing.GetComponent<Interactable>().UseText;
        }
        else
        {
            if (CurrentlyUsing != null)
            {
            CurrentlyUsing = null;
            GameControl.UseHintText.text = "";
            }
        }
    }
}
