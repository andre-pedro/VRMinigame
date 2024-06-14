using UnityEngine;

public class FlowerInCauldron : MonoBehaviour
{
    public string requiredIngredient; // Name of the required ingredient
    public PotionMaker potionMaker; // Reference to the PotionMaker script

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.name); // Check what object is colliding

        // Check if the collided object has the required ingredient component
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null && ingredient.ingredientName == requiredIngredient)
        {
            Debug.Log(requiredIngredient + " collided with " + gameObject.name);

            // Destroy the ingredient GameObject
            Destroy(other.gameObject);

            // Check if PotionMaker script is assigned
            if (potionMaker != null)
            {
                potionMaker.OnFlowerInCauldronEnter(); // Call method in PotionMaker script
            }
            else
            {
                Debug.LogWarning("PotionMaker script reference not set in " + gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit: " + other.name); // Check what object is exiting

        // Check if the collided object has the required ingredient component
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null && ingredient.ingredientName == requiredIngredient)
        {
            Debug.Log(requiredIngredient + " exited " + gameObject.name);

            // Check if PotionMaker script is assigned
            if (potionMaker != null)
            {
                potionMaker.OnFlowerInCauldronExit(); // Call method in PotionMaker script
            }
            else
            {
                Debug.LogWarning("PotionMaker script reference not set in " + gameObject.name);
            }
        }
    }
}
