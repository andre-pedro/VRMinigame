using UnityEngine;

public class PotionStir : MonoBehaviour
{
    public string requiredIngredient; // Name of the required ingredient
    private bool ingredientAdded = false; // Flag to track if the ingredient has been added
    private bool isHoldingObject = false; // Flag to track if the object is being held
    private float holdTimer = 0f; // Timer to track how long the object has been held
    public float requiredHoldTime = 6f; // Time in seconds required to hold the object
    public GameObject potionPrefab; // Prefab of the potion to instantiate
    public GameObject incorrectEffectPrefab; // Prefab of the particle effect to instantiate
    public Vector3 spawnOffset = new Vector3(0f, 1.0f, 0f); // Adjustable spawn offset
    public GameObject objectToHold; // Reference to the object that needs to be held

    private void Start()
    {
        // Ensure requiredIngredient is not null or empty
        if (string.IsNullOrEmpty(requiredIngredient))
        {
            Debug.LogError("Required ingredient is not set in the Cauldron script!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is an ingredient
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null && ingredient.ingredientName == requiredIngredient)
        {
            ingredientAdded = true;
            Debug.Log(requiredIngredient + " added to the cauldron.");
            CheckConditions();
        }
    }

    private void Update()
    {
        // Check if the object to hold is being held
        if (IsObjectBeingHeld())
        {
            holdTimer += Time.deltaTime;

            // Check if the required hold time is reached
            if (holdTimer >= requiredHoldTime)
            {
                Debug.Log("Object held for " + requiredHoldTime + " seconds! Potion created.");
                CreatePotion();

                // Reset hold state
                ResetHoldState();
                CheckConditions(); // Check if the ingredient condition is also met
            }
        }
        else
        {
            // Reset hold state if the object is not being held anymore
            ResetHoldState();
        }
    }

    private bool IsObjectBeingHeld()
    {
        // Replace this with your actual logic to determine if the object is being held
        if (objectToHold != null && objectToHold.activeSelf)
        {
            return true;
        }
        return false;
    }

    private void CreatePotion()
    {
        // Instantiate the potion at the cauldron's position
        Instantiate(potionPrefab, transform.position + Vector3.up, Quaternion.identity);
    }

    private void ResetHoldState()
    {
        // Reset hold timer
        holdTimer = 0f;
    }

    private void CheckConditions()
    {
        // Check if both conditions are met: ingredient added and object held for required time
        if (ingredientAdded && holdTimer >= requiredHoldTime)
        {
            Debug.Log("Conditions met! Creating potion.");
            CreatePotion();
            ResetCauldron();
        }
    }

    private void ResetCauldron()
    {
        // Reset flags and states
        ingredientAdded = false;
        ResetHoldState();
    }
}
