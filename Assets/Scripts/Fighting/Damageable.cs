
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Health health;

    public void TakeDamage(int damageAmount)
    {
        health.SubtractHealth(damageAmount);
    }
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

}
