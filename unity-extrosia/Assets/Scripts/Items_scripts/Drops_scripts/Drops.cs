using System.Collections;
using System.Collections.Generic;
using Items.Drops;
using UnityEngine;
using Types = Items.Drops.Types;

public class Drops : MonoBehaviour
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
                    if (stats.maxHealth > stats.currentHealth+amount)
                    {
                        stats.currentHealth += amount;
                        stats.healthBar.SetHealth(stats.currentHealth);
                    }
                    break;
                case Types.Mana:
                    if (stats.maxMana > stats.currentMana+amount)
                    {
                        stats.currentMana += amount;
                        stats.manaBar.SetMana(stats.currentMana);
                    }
                    break;
                case Types.Exp:
                    stats.maxHealth += amount;
                    stats.healthBar.SetMaxHealth(stats.maxHealth);
                    stats.maxMana += amount;
                    stats.manaBar.SetMaxMana(stats.maxMana);
                    break;
                case Types.Relics:
                    stats.currentFinalObjects += 1;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
