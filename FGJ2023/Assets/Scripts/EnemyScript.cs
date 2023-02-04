using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Health, WoodGained;
    private bool invincible;
    public float InvincibleTime;
    public GameControl GameControl;

    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !invincible)
        {
            invincible = true;
            StartCoroutine(HitCd());
            Health -= GameControl.AxeDamage;
            GameControl.DamageAxe();

            if (Health <= 0)
            Die();
        }
    }

    private IEnumerator HitCd()
    {
        yield return new WaitForSeconds(InvincibleTime);
        invincible = false;
    }

    public virtual void Die() 
    {
        GameControl.GetLumber(WoodGained);
        EnemySpawner.EnemiesTotalCurr--;
        ObjectPooler.Destroy(this.gameObject);
    }
}
