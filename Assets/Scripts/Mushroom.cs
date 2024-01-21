using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float attackDamage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerDetection"))
        {

        }
        else if (collider.gameObject.CompareTag("PlayerHitBox"))
        {
            LifesSystem lifesSystem = collider.gameObject.GetComponent<LifesSystem>();
            lifesSystem.ReceiveDamage(attackDamage);
        }
    }
}