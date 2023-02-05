using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class EnemyScript : MonoBehaviour
{
    public int Health, WoodGained;
    private bool invincible, leeching;
    public float InvincibleTime, Speed;
    public GameControl GameControl;
    public Material GlowyMat;
    public bool active = true;
    Transform crystal;
    ParticleSystem WoodChips;

    Transform startPos;

    bool firstSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
        GameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
    }

    protected virtual void OnEnable()
    {

        if (firstSpawn)
        {
            startPos = this.gameObject.transform;
            firstSpawn = false;
        }

        this.gameObject.transform.position = startPos.position;
        this.gameObject.transform.rotation = startPos.rotation;

        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        WoodChips = GameObject.FindGameObjectWithTag("WoodChips").GetComponent<ParticleSystem>();
        //this.transform.parent.DOMove(crystal.position, TimeToReachCrystal).SetEase(Ease.InSine);
        active = true;
        MasterAudio.PlaySound3DFollowTransform("sfx_roots_low", this.gameObject.transform);
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
            transform.position = Vector3.MoveTowards(startPos.position, crystal.position, (Speed + GameControl.DarknessLevel) * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.gameObject.layer == 6)
            {
                WoodChips.Play();
                invincible = true;
                StartCoroutine(HitCd());
                Health -= GameControl.AxeDamage;
                GameControl.DamageAxe();
                MasterAudio.PlaySound3DAtTransformAndForget("sfx_axe_hit", other.gameObject.transform);

                if (Health <= 0)
                    Die();
            }

            if (other.gameObject.tag == "Crystal" && !leeching)
            {
                StartLeeching();
                MasterAudio.PlaySound3DAtTransformAndForget("sfx_crystal_damaged", other.gameObject.transform);
            }
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

    public virtual void Die()
    {
        active = false;
        leeching = false;

        if (GameControl.RootsOnCrystal > 0)
        GameControl.RootsOnCrystal--;

        GameControl.GetLumber(WoodGained);

        EnemySpawner.EnemiesTotalCurr--;
        MasterAudio.StopAllSoundsOfTransform(this.gameObject.transform);
        StartCoroutine(MasterAudio.PlaySound3DAtTransformAndWaitUntilFinished("sfx_root_death", this.gameObject.transform));
        
        ObjectPooler.Destroy(this.transform.parent.gameObject);
    }
}
