using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = waypoints[currentIndex].position;
        healthBar.maxValue = health;
        healthBar.value = health;
        anim = GetComponent<Animator>();
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            anim.SetBool("running", true);
            while (transform.position != currentTarget)
            {
                if (!playerDetected && !hasRecibedDamage)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget, patrolSpeed * Time.deltaTime);
                    yield return null;
                }
                else if (hasRecibedDamage)
                {
                    yield return new WaitForSeconds(0.5f);
                    hasRecibedDamage = false;
                }
                else
                {
                    while (playerDetected)
                    {
                        anim.SetBool("running", true);
                        FocusPlayer();
                        while ((transform.position.x < player.transform.position.x - 2f || transform.position.x > player.transform.position.x + 2f)
                            && playerDetected)
                        {
                            float destinationX = Mathf.Clamp(player.transform.position.x, waypoints[1].position.x, waypoints[0].position.x);

                            Vector3 objetive = new Vector3(destinationX, transform.position.y, 0f);
                            transform.position = Vector3.MoveTowards(transform.position, objetive, patrolSpeed * Time.deltaTime);
                            yield return null;
                        }
                        if (playerDetected)
                        {
                            FocusPlayer();
                            anim.SetBool("running", false);
                            yield return new WaitForSeconds(0.5f);
                            anim.SetTrigger("attack");
                            yield return new WaitForSeconds(1f);
                        }
                    }
                    anim.SetBool("running", true);
                    FocusTarget();
                }
            }
            anim.SetBool("running", false);
            yield return new WaitForSeconds(3f);
            DefineNewTarget();
        }
    }
}
