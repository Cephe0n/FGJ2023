using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

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
