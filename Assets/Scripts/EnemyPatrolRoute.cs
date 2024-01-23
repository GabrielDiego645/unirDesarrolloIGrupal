using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolRoute : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed;

    private Vector3 currentTarget;
    private int currentIndex = 0;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentTarget = waypoints[currentIndex].position;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            anim.SetBool("running", true);
            while (transform.position != currentTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, patrolSpeed * Time.deltaTime);
                yield return null;
            }
            anim.SetBool("running", false);
            yield return new WaitForSeconds(3f);
            DefineNewTarget();
        }
    }

    private void DefineNewTarget()
    {
        currentIndex++;
        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 0;
        }
        currentTarget = waypoints[currentIndex].position;
        FocusTarget();
    }

    private void FocusTarget()
    {
        if (currentTarget.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
