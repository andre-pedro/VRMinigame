using UnityEngine;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour
{
    public List<string> correctOrder; // The correct order of ingredients
    private List<string> currentOrder = new List<string>(); // The current order of ingredients added
    public GameObject potionPrefab; // Prefab of the potion to instantiate
    public GameObject incorrectEffectPrefab; // Prefab of the particle effect to instantiate
    public Vector3 spawnOffset = new Vector3(0f, 1.0f, 0f); // Adjustable spawn offset

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
                    Debug.Log("Correct order! Potion created.");
                    CreatePotion();
                }
                else
                {
                    Debug.Log("Incorrect order. Try again.");
                    CreateIncorrectEffect();
                }
                ResetCauldron();
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

    private void CreatePotion()
    {
        // Instantiate the potion at the cauldron's position
        Instantiate(potionPrefab, transform.position + Vector3.up, Quaternion.identity);
    }

    private void CreateIncorrectEffect()
    {
        // Instantiate the particle effect at the cauldron's position with an offset
        Vector3 spawnPosition = transform.position + spawnOffset;
        Instantiate(incorrectEffectPrefab, spawnPosition, Quaternion.identity);
    }

    private void ResetCauldron()
    {
        // Clear the current order
        currentOrder.Clear();

        // Optionally, destroy the ingredients in the cauldron
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