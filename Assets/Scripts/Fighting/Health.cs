
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializeField] private int maxHealth;
   private int currentHealth;


   public event Action OnHealthDepleted;
   private void Start()
   {
      currentHealth = maxHealth;
     
      
   }

   public void SubtractHealth(int amount)
   {
      currentHealth -= amount;
      if (currentHealth <= 0) OnHealthDepleted?.Invoke();
   }

   public void AddHealth(int amount)
   {
      currentHealth += amount;
   }
}
