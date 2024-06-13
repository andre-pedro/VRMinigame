using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlane : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        // Store the original positions of all objects tagged as "ResettableObject"
        GameObject[] resettableObjects = GameObject.FindGameObjectsWithTag("ResettableObject");
        foreach (GameObject obj in resettableObjects)
        {
            originalPositions[obj] = obj.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is one that should be reset
        if (other.gameObject.CompareTag("ResettableObject"))
        {
            // Reset the object's position to its original position
            if (originalPositions.ContainsKey(other.gameObject))
            {
                other.transform.position = originalPositions[other.gameObject];
                Debug.Log($"{other.gameObject.name} has been reset to its original position.");
            }
        }
    }
}