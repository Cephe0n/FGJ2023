using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    public int Health, WoodGained;
    private bool invincible, leeching;
    public float InvincibleTime, TimeToReachCrystal;
    public GameControl GameControl;
    public Material GlowyMat;
    Transform crystal;

    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
    }

    void OnEnable()
    {
        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        this.transform.parent.DOMove(crystal.position, TimeToReachCrystal);
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

        if (other.gameObject.tag == "Crystal" && !leeching)
        {
            StartLeeching();
        }
    }

    void StartLeeching()
    {
        leeching = true;
        GetComponent<MeshRenderer>().material = GlowyMat;
    }

    private IEnumerator HitCd()
    {
        yield return new WaitForSeconds(InvincibleTime);
        invincible = false;
    }

    public virtual void Die()
    {
        leeching = false;
        GameControl.GetLumber(WoodGained);
        EnemySpawner.EnemiesTotalCurr--;
        ObjectPooler.Destroy(this.transform.parent.gameObject);
    }
}
