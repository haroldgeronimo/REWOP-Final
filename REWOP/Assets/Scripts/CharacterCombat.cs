using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackdelay = .6f;

    public event System.Action OnAttack;
    CharStats myStats;
        private void Start()
    {
        myStats = GetComponent<CharStats>();
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public void Attack(CharStats targetStats) {
        if (attackCooldown <= 0f) {
            StartCoroutine(DoDamage(targetStats,attackdelay));

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
        }
       
    }

    public void MultiAttack(List<CharStats> targetStatuses)
    {
        if (attackCooldown <= 0f)
        {
            foreach (CharStats targetStats in targetStatuses)
            {
                StartCoroutine(DoDamage(targetStats, attackdelay));
            }
            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
        }

    }

    IEnumerator DoDamage (CharStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());

		SFXPlaying.instance.PlaySound (SFXPlaying.instance.Attack);
    }



    IEnumerator DoMultiDamage(List <CharStats> stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (CharStats stat in stats) {


            stat.TakeDamage(myStats.damage.GetValue());
        }


    }
}
