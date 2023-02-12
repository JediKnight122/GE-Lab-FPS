
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Health health;

    public UnityEvent OnHit;
    
    public void TakeDamage(int damageAmount)
    {
        health.SubtractHealth(damageAmount);
        OnHit?.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

}
