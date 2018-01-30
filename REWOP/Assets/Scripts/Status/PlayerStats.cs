using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharStats {
    public HurtScript Hurt;
    public FacialScript facial;
    private void Start()
    {
                StartCoroutine(HurtUI());
    }
    public override void Die()
    {
        base.Die();
        //Kill player
        PlayerManager.instance.KillPlayer();
    }
    public override bool Heal(int amount)
    {
        if (base.Heal(amount)) return true;
        else return false;
    }
    public void KillPlayer()
    {
        TakeDamage(maxHealth);
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;
    }
    public override void TakeDamage(int damage)
    {
        Hurt.HurtAccentShow(damage);
        base.TakeDamage(damage);
        IsHurt = true;
    }
    public override void SetCurrentHealth(int amount)
    {
        base.SetCurrentHealth(amount);
    }
    bool IsHurt = false;
    public IEnumerator HurtUI()
    {
        while (true)
        {
            int spriteCount = facial.facials.Count;
            int interval = (currentHealth / spriteCount) - 1;


            
            if(currentHealth <= 0)
            {
                facial.ShowDead();
                yield return new WaitForSeconds(0.5f);
                continue;
            }


            if (IsHurt)
            {
                facial.ShowDamage(interval);
                IsHurt = false;
            }
            else
            {
                facial.ShowNormal(interval);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
