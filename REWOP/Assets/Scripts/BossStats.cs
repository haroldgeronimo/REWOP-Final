using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossStats :CharStats {
    public Image HealthBar;
    public Animator enemyAnimator;
    public EnemyTypes enemyType;
    public void Start()
    {
        enemyAnimator = transform.GetComponentInChildren<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        
        base.TakeDamage(damage);
        float ch = currentHealth;
        float mh = maxHealth;
        HealthBar.fillAmount = ch / mh;
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine(BossDie());
        if(enemyType == EnemyTypes.DRONE)
         AchievementData.instance.UpdateCounterByTag("killVirusDrone", 1);
        else if (enemyType == EnemyTypes.ROOK)
            AchievementData.instance.UpdateCounterByTag("killVirusRook", 1);
        else if (enemyType == EnemyTypes.MASTER)
            AchievementData.instance.UpdateCounterByTag("killVirusMaster", 1);

    }
    IEnumerator BossDie() { 
        enemyAnimator.SetTrigger("IsDie");
        yield return new WaitForSeconds(5f);
        Destroy(this);
        yield return null;
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;
    }
}

public enum EnemyTypes
{
    DRONE,
    ROOK,
    MASTER
}