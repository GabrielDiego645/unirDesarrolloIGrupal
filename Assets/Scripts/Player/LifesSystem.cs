using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Vector3 playerInitialPosition;
    private Animator anim;
    private float maxHealth;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar.maxValue = health;
        healthBar.value = health;
        maxHealth = health;
        playerInitialPosition = transform.position;
    }

    public IEnumerator ReceiveDamage(float damageRecibed)
    {
        health -= damageRecibed;
        healthBar.value = health;
        rb.velocity = new Vector2(0f, 0f);
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
                    SceneManager.LoadScene(5);
                    break;
            }
            numLifes--;
        }
    }

    private void Restart()
    {
        transform.SetPositionAndRotation(playerInitialPosition, Quaternion.identity);

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        health = maxHealth;
    }
}
