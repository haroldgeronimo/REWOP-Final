using UnityEngine;

public class CharStats : MonoBehaviour {
    public int maxHealth = 100;
    public int currentHealth { get; set; }
    public Stat damage;
    public Stat armor;
   
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public virtual void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0) {
            Die();
        }
    }
    public virtual bool Heal(int amount) {
        currentHealth += amount;
        Debug.Log(transform.name + " takes " + amount + " health.");

        if (maxHealth <= currentHealth)
        {
            currentHealth = maxHealth;
            return false;
        }

        return true;

    }
    public virtual void SetCurrentHealth(int amount)
    {
        currentHealth = amount;
    }

    public virtual void Die()
    {
        // this is for overriding
        Debug.Log(transform.name + "died");
    }


    

   
}
