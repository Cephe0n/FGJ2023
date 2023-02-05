using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    public int Health, WoodGained;
    private bool invincible, leeching;
    public float InvincibleTime, Speed;
    public GameControl GameControl;
    public Material GlowyMat;
    bool active;
    Transform crystal;
    ParticleSystem WoodChips;

    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
    }

    void OnEnable()
    {
        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        WoodChips = GameObject.FindGameObjectWithTag("WoodChips").GetComponent<ParticleSystem>();
        //this.transform.parent.DOMove(crystal.position, TimeToReachCrystal).SetEase(Ease.InSine);
        active = true;
    }

    void OnDisable()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !leeching)
        {
            transform.position = Vector3.MoveTowards(transform.position, crystal.position, (Speed + GameControl.DarknessLevel) * Time.deltaTime);
        }

        if (Health <= 0)
            Die();

        if (!GameControl.WaveActive)
            Die(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !invincible)
        {
            WoodChips.Play();
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
        GameControl.RootsOnCrystal++;
        GetComponent<MeshRenderer>().material = GlowyMat;
    }

    private IEnumerator HitCd()
    {
        yield return new WaitForSeconds(InvincibleTime);
        invincible = false;
    }

    public virtual void Die(bool fromwaveover = false)
    {
        active= false;
        leeching = false;
        GameControl.RootsOnCrystal--;
        if (!fromwaveover)
        GameControl.GetLumber(WoodGained);

        EnemySpawner.EnemiesTotalCurr--;
        ObjectPooler.Destroy(this.transform.parent.gameObject);
    }
}
