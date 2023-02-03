using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Health;
    private AxeActions axeScript;
    private bool invincible;
    public float InvincibleTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !invincible)
        {
            invincible = true;
            StartCoroutine(HitCd());
            axeScript = other.GetComponent<AxeActions>();
            Health -= axeScript.Damage;

            if (Health <= 0)
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator HitCd()
    {
        yield return new WaitForSeconds(InvincibleTime);
        invincible = false;
    }
}
