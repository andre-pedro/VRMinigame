using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionStir : MonoBehaviour
{
    public GameObject potionPrefab; // Prefab of the potion to instantiate

    private XRGrabInteractable grabInteractable;
    private float holdTime = 0f;
    private bool holdingObject = false;

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
        if (holdingObject)
        {
            holdTime += Time.deltaTime;

            // Check if hold time exceeds 6 seconds
            if (holdTime >= 6f)
            {
                CreatePotion();
                holdTime = 0f; // Reset hold time
                holdingObject = false; // Stop tracking time
            }
        }
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        holdingObject = true;
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        holdTime = 0f; // Reset hold time if released before 6 seconds
        holdingObject = false;
    }

    private void CreatePotion()
    {
        // Instantiate the potion at the object's position
        Instantiate(potionPrefab, transform.position, Quaternion.identity);
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
