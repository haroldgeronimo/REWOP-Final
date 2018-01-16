using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharStats {
    private void Start()
    {
        Load();
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
    public void Load()//Set player health
    {
        if (SaveLoadManager.Instance != null)
            if (SaveLoadManager.Instance.gameData != null)
        {
            SetCurrentHealth(SaveLoadManager.Instance.gameData.PlayerLife);
        }
    }

    public override void SetCurrentHealth(int amount)
    {
        base.SetCurrentHealth(amount);
    }
}
