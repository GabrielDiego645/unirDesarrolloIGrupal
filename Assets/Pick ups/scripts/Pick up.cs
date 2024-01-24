using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        bool reached = false;

        // Update our progress to our destination
        progress += speed * Time.deltaTime;

        // Check for the case when we overshoot or reach our destination
        if (progress >= 1.0f)
        {
            progress = 1.0f;
            reached = true;
        }

        // Update out position based on our start postion, destination and progress.
        transform.localPosition = (destination * progress) + start * (1 - progress);

        // If we have reached the destination, set it as the new start and pick a new random point. Reset the progress
        if (reached)
        {
            start = destination;
            PickNewRandomDestination();
            progress = 0.0f;
        }

        RotateGameObject(destination, RotationSpeed, rotOffset);
    }

    void PickNewRandomDestination()
    {
        // We add basestartpoint to the mix so that is doesn't go around a circle in the middle of the scene.
        destination = Random.insideUnitCircle * radius + basestartpoint;
    }

    private void RotateGameObject(Vector2 destination, float RotationSpeed, float offset)
    {
        Vector3 target = destination;
        //get the direction of the other object from current object
        Vector3 dir = target - transform.position;
        //get the angle from current direction facing to desired target
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //set the angle into a quaternion + sprite offset depending on initial sprite facing direction
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        
    }
