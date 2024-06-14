using UnityEngine;

public class PotionOnPedestal : MonoBehaviour
{
    public GameObject canvas; // Reference to the canvas to show

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the Potion component
        Potion potion = other.GetComponent<Potion>();
        if (potion != null)
        {
            // Activate the canvas
            canvas.SetActive(true);

            // Freeze the potion's movement
            Rigidbody potionRigidbody = other.GetComponent<Rigidbody>();
            if (potionRigidbody != null)
            {
                potionRigidbody.isKinematic = true;
            }
        }
        else
        {
            Debug.Log("Incorrect object collided.");
        }
    
    }
}
