using System.Collections;
using System.Collections.Generic;
using Items.Drops;
using UnityEngine;
using Types = Items.Drops.Types;

public class Drop : MonoBehaviour
{

    public Types type;
    public int amount;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var stats = other.gameObject.GetComponent<Stats>();
            switch (type)
            {
                case Types.Health:
                    stats.currentHealth += amount;
                    stats.healthBar.SetHealth(stats.currentHealth);
                    break;
                case Types.Mana:
                    stats.currentMana += amount;
                    stats.manaBar.SetMana(stats.currentMana);
                    break;
                case Types.Exp:
                    stats.maxHealth += amount;
                    stats.healthBar.SetMaxHealth(stats.maxHealth);
                    stats.maxMana += amount;
                    stats.manaBar.SetMaxMana(stats.maxMana);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
