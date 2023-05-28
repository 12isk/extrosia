using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public int maxHealth;
    public int maxMana;
    
    public int currentHealth;
    public int currentMana;
    public int currentFinalObjects;

    public bool hasAllItems;
    
    public HealthBar healthBar;

    public ManaBar manaBar;
    public bool canAttack;

    public Attack attack;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        canAttack = true;
        //todo: add max mana
        manaBar.SetMaxMana(maxMana);
        healthBar.SetMaxHealth(maxHealth);
        
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void ManaAttack(int amount)
    {
        currentMana -= amount;
        manaBar.SetMana(currentMana);
    }
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakeDamage(20);
        // }

        hasAllItems = currentFinalObjects >= 3;
        canAttack = currentMana >= 0;

        if (hasAllItems)
        {
            //todo: add win thing here
            Debug.Log("You have all the items");
        }

        //todo: add mana reducing stuff

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            ManaAttack(5);
        }
        
        if (Input.GetButtonDown("Fire2") && canAttack)
        {
            ManaAttack(20);
        }

    }
    
    public void ExpandManaLimit(int amount)
    {
        maxMana += amount*2;
        manaBar.SetMaxMana(maxMana);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("QuestObject"))
        {
            currentFinalObjects += 1;
            Destroy(collision.gameObject);
        }
        
    }
}
