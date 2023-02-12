using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Animator animator;
    private RectTransform rectTransform;
    private float initialY;
    private float initialX;
    private void Start()
    {
        rectTransform = healthBar.GetComponent<RectTransform>();
        initialY = rectTransform.localPosition.y;
        initialX = rectTransform.localPosition.x;
    }

    public void UpdateHealthDisplay(int pHealthAmount)
    {
        animator.SetTrigger("Hit");
        Debug.Log("Moving Health Image...");
       rectTransform.localPosition =  new Vector3(initialX, initialY-((100-pHealthAmount)*20));
       // healthBar.transform.position.Set( healthBar.transform.position.x,  healthBar.transform.position.y-((100-pHealthAmount)*2),  healthBar.transform.position.y);
    }
 //rectTransform.position.y-((100-pHealthAmount)*2)
}
