using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyStats : CharStats{
    public Image HealthBar;
    public EnemyAnimator enemyAnimator;
    public NavMeshAgent enemyNavigator;
    public CharacterCombat enemyCombat;
    public EnemyController enemyController;
    public void Start()
    {
        enemyAnimator = this.GetComponent<EnemyAnimator>();
        enemyNavigator = this.GetComponent<NavMeshAgent>();
        enemyCombat = this.GetComponent<CharacterCombat>();
        enemyController = this.GetComponent<EnemyController>();
    }

    public override void TakeDamage(int damage)
    {
        enemyAnimator.EnemyHit();
        StartCoroutine(DisableHit());
        base.TakeDamage(damage);
        float ch = currentHealth;
        float mh = maxHealth;
        HealthBar.fillAmount = ch / mh;
    }
    
    public override void Die() {
     
        base.Die();
        Transform player = PlayerManager.instance.player.transform;
        CollisionObjects ColObj = player.GetChild(1).GetComponent<CollisionObjects>();
        ColObj.items.Remove(this.gameObject);
        
        // Add ragdoll effect
        enemyAnimator.EnemyDie();
        enemyNavigator.enabled = false;
        enemyCombat.enabled = false;
        enemyController.enabled = false;
        StartCoroutine(DestroyEnemy());
        AchievementData.instance.UpdateCounterByTag("killVirus", 1);
    }
    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        yield return null;
    }
    IEnumerator DisableHit()
    {
        yield return new WaitForSeconds(.5f);
        enemyAnimator.EnemyNotHit();
        yield return null;
    }
}
