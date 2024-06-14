using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionMaker : MonoBehaviour
{
    public GameObject potionPrefab; // Prefab of the potion to instantiate

    private XRGrabInteractable grabInteractable;
    private float holdTime = 0f;
    private bool holdingObject = false;
    private bool flowerInCauldron = false; // Flag to track if flower is in the cauldron

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("PotionMaker script requires XRGrabInteractable component.");
            enabled = false;
            return;
        }

        // Subscribe to grab events
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    private void Update()
    {
        if (holdingObject && flowerInCauldron) // Check if holding object and flower is in cauldron
        {
            holdTime += Time.deltaTime;

            // Check if hold time exceeds 6 seconds
            if (holdTime >= 6f)
            {
                CreatePotion();
                ResetPotionCreation(); // Reset variables after potion creation
            }
        }
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        holdingObject = true;

        // Enable Rigidbody when grabbed to allow physics interactions
        Rigidbody rb = interactor.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Enable Rigidbody to allow physics interactions
        }
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        ResetPotionCreation(); // Reset variables if released before potion creation
    }

    private void CreatePotion()
    {
        // Instantiate the potion at the object's position
        GameObject potionObject = Instantiate(potionPrefab, transform.position, Quaternion.identity);

        // Disable Rigidbody initially to freeze the potion
        Rigidbody potionRb = potionObject.GetComponent<Rigidbody>();
        if (potionRb != null)
        {
            potionRb.isKinematic = true; // Disable Rigidbody to freeze the potion
        }
    }

    private void ResetPotionCreation()
    {
        holdTime = 0f; // Reset hold time
        holdingObject = false; // Stop tracking time
    }

    // Method called when flower enters the cauldron trigger
    public void OnFlowerInCauldronEnter()
    {
        Debug.Log("Flower entered the cauldron.");
        flowerInCauldron = true;
    }

    // Method called when flower exits the cauldron trigger
    public void OnFlowerInCauldronExit()
    {
        Debug.Log("Flower exited the cauldron.");
        flowerInCauldron = false;
    }

    private void OnDestroy()
    {
        // Clean up event listeners
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.RemoveListener(OnGrab);
            grabInteractable.onSelectExited.RemoveListener(OnRelease);
        }
    }
}

