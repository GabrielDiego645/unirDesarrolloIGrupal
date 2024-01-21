using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesSystem : MonoBehaviour
{
    [SerializeField] private float health;

    public void ReceiveDamage(float danhoRecibido)
    {
        Debug.Log("Actor damaged");
        health -= danhoRecibido;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
