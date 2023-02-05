using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using DarkTonic.MasterAudio;

public class AxeActions : MonoBehaviour
{

    public BoxCollider AxeHitbox;
    public bool AttackOnCooldown;
    public AnimancerComponent Animancer;
    public AnimationClip AxeAttackAnim;

    public void Attack()
    {
        if (!AttackOnCooldown)
        {
            AttackOnCooldown = true;
            Animancer.Play(AxeAttackAnim);
            MasterAudio.PlaySound3DAtTransformAndForget("sfx_axe_swing", this.gameObject.transform);
        }
    }

    public void ResetCd()
    {
        AttackOnCooldown = false;
        Animancer.Stop();
    }

    public void ToggleHitbox()
    {
        AxeHitbox.enabled = !AxeHitbox.enabled;
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
