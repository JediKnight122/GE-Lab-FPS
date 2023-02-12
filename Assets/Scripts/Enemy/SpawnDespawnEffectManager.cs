using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDespawnEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private float animationSpeed = 0.05f;
    [SerializeField] private ParticleSystem deathParticles;
    private Material dissolveEffect;

    private float amount = -1;
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }
    private void OnEnable()
    {
        GetComponent<Health>().OnHealthDepleted += StartDespawnEffect;
        dissolveEffect = body.GetComponent<SkinnedMeshRenderer>().materials[1];
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnHealthDepleted -= StartDespawnEffect;
    }

    private void StartDespawnEffect()
    {
        deathParticles.Play();
        StartCoroutine(Despawn());
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);
        amount = -1;
        while (amount < 1)
        {
            amount += animationSpeed*0.7f;
            dissolveEffect.SetFloat("_Change_Slider", amount);
            yield return new WaitForSeconds(animationSpeed);
        }

        amount = 1;
        dissolveEffect.SetFloat("_Change_Slider", amount);
    }
    IEnumerator Spawn()
    {
        amount = 1;
        while (amount > -1)
        {
            amount -= animationSpeed;
            dissolveEffect.SetFloat("_Change_Slider", amount);
            yield return new WaitForSeconds(animationSpeed);
        }
        amount = -1;
        dissolveEffect.SetFloat("_Change_Slider", amount);
    }
}
