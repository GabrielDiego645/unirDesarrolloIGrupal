using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesSystem : MonoBehaviour
{
    [SerializeField] private float health;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator ReceiveDamage(float damageRecibed)
    {
        Debug.Log("Actor damaged");
        health -= damageRecibed;
        if (health > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            anim.SetTrigger("death");
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
    }
}
