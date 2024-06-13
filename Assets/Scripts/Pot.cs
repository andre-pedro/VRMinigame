using UnityEngine;
using System.Collections.Generic;

public class Pot : MonoBehaviour
{
    public List<string> correctOrder; // The correct order of ingredients
    private List<string> currentOrder = new List<string>(); // The current order of ingredients added
    public GameObject plantPrefab; // Prefab of the plant to instantiate

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is an ingredient by looking for the Ingredient component
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            // Get the ingredient's name from the Ingredient component
            string ingredientName = ingredient.ingredientName;

            // Add the ingredient to the current order
            currentOrder.Add(ingredientName);

            // Check if the current order matches the correct order
            if (currentOrder.Count == correctOrder.Count)
            {
                if (IsCorrectOrder())
                {
                    Debug.Log("Correct order! Plant created.");
                    CreatePlant();
                    ResetPot();
                }
                else
                {
                    Debug.Log("Incorrect order. Try again.");
                    ResetPot();
                }
            }
        }
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
        Vector3 spawnPosition = transform.position + Vector3.up * 0.12f; // Adjust the offset as needed to prevent plant from spawing far from correct location
        Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
    }

    private void ResetPot()
    {
        // Clear the current order
        currentOrder.Clear();

        // Optionally, destroy the ingredients in the pot
        // You can adjust this part based on how you want to handle the ingredients
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Ingredient>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}