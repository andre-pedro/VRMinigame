using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnOnInteract : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of object prefabs to spawn
    public Transform[] spawnPoints; // Array of spawn points

    private XRSimpleInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRSimpleInteractable>();

        // Subscribe to the interactable's selectEntered event
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        // Iterate through each spawn point and spawn the corresponding object
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject objectPrefab = objectsToSpawn[i % objectsToSpawn.Length]; // Cycle through object prefabs
            Transform spawnPoint = spawnPoints[i];

            // Spawn the object prefab at the current spawn point
            Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

