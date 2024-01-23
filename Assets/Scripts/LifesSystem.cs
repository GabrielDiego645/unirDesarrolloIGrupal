using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifesSystem : MonoBehaviour
{
    [Header("Lifes System")]
    [SerializeField] private float health;
    [SerializeField] protected Slider healthBar;
    [SerializeField] private Image life1;
    [SerializeField] private Image life2;
    [SerializeField] private Image life3;

    private float numLifes = 3;
    private Transform playerInitialTransform;
    private Animator anim;
    private float maxHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
        healthBar.maxValue = health;
        healthBar.value = health;
        maxHealth = health;
        playerInitialTransform = transform;
    }

    public IEnumerator ReceiveDamage(float damageRecibed)
    {
        Debug.Log("Actor damaged");
        health -= damageRecibed;
        healthBar.value = health;
        if (health > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            anim.SetTrigger("death");
            yield return new WaitForSeconds(0.5f);
            switch (numLifes) 
            {
                case 3:
                    life1.enabled = false;
                    Restart();
                    break;
                case 2:
                    life2.enabled = false;
                    Restart();
                    break;
                case 1:
                    life3.enabled = false;
                    Destroy(this.gameObject);
                    break;
            }
            numLifes--;
        }
    }

    private void Restart()
    {
        transform.eulerAngles = playerInitialTransform.eulerAngles;
        transform.position = playerInitialTransform.position;

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        health = maxHealth;
    }
}
