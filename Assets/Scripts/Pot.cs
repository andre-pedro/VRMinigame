using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pot : MonoBehaviour
{
    public List<string> correctOrder; // The correct order of ingredients
    private List<string> currentOrder = new List<string>(); // The current order of ingredients added
    public GameObject plantPrefab; // Prefab of the plant to instantiate

    public GameObject incorrectEffectPrefab; // Prefab to instantiate when incorrect order is detected

    private bool isCooldown = false; // Cooldown flag

    // Dictionary to store initial positions of ingredients
    private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        // Store initial positions of all child objects that are ingredients
        StoreInitialPositions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCooldown) return; // If cooldown is active, do nothing

        // Check if the object entering the trigger is an ingredient by looking for the Ingredient component
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            StartCoroutine(Cooldown()); // Start cooldown coroutine

            // Get the ingredient's name from the Ingredient component
            string ingredientName = ingredient.ingredientName;

            // Add the ingredient to the current order
            currentOrder.Add(ingredientName);

            // Disable MeshRenderer and BoxCollider
            DisableRenderingAndCollision(ingredient.gameObject);

            // Reset ingredient to initial position after 1 second
            StartCoroutine(ResetToInitialPositionAfterDelay(ingredient.gameObject, 1f));

            // Check if the current order matches the correct order
            if (currentOrder.Count == correctOrder.Count)
            {
                if (IsCorrectOrder())
                {
                    Debug.Log("Correct order! Plant will be created.");
                    StartCoroutine(CreatePlantWithDelay());
                }
                else
                {
                    Debug.Log("Incorrect order. Try again.");
                    Instantiate(incorrectEffectPrefab, transform.position, Quaternion.identity);
                    ResetPot();
                }
            }
        }
    }

    private void DisableRenderingAndCollision(GameObject ingredient)
    {
        var renderer = ingredient.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false; // Disable MeshRenderer
        }

        var collider = ingredient.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = false; // Disable BoxCollider
        }
    }

    private void StoreInitialPositions()
    {
        // Store initial positions of all child objects that are ingredients
        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            if (obj.GetComponent<Ingredient>() != null)
            {
                initialPositions[obj] = obj.transform.position;
            }
        }
    }

    private IEnumerator ResetToInitialPositionAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for specified delay

        // Reset object to its initial position
        if (initialPositions.ContainsKey(obj))
        {
            obj.transform.position = initialPositions[obj];
        }

        // Re-enable MeshRenderer and BoxCollider
        var renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = true; // Enable MeshRenderer
        }

        var collider = obj.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = true; // Enable BoxCollider
        }
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true; // Activate cooldown
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        isCooldown = false; // Deactivate cooldown
    }

    private IEnumerator CreatePlantWithDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before creating the plant
        CreatePlant(); // Call CreatePlant after the delay
        ResetPot(); // Reset the pot after creating the plant
    }

    private bool IsCorrectOrder()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                return false;
            }
        }
        return true;
    }

    private void CreatePlant()
    {
        // Instantiate the plant at the pot's position
        Vector3 spawnPosition = transform.position + Vector3.up * 0.12f; // Adjust the offset as needed to prevent plant from spawning far from the correct location
        Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
    }

    private void ResetPot()
    {
        // Clear the current order
        currentOrder.Clear();

        // Optionally, reset the ingredients in the pot to their initial positions
        foreach (KeyValuePair<GameObject, Vector3> pair in initialPositions)
        {
            GameObject obj = pair.Key;
            if (obj != null)
            {
                obj.transform.position = pair.Value; // Reset position
                var renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = true; // Enable MeshRenderer
                }

                var collider = obj.GetComponent<BoxCollider>();
                if (collider != null)
                {
                    collider.enabled = true; // Enable BoxCollider
                }
            }
        }
    }
}
