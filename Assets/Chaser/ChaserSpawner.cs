using UnityEngine;

public class ChaserSpawner : MonoBehaviour
{

    public GameObject chaserPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void SpawnChaser()
    {
        Instantiate(chaserPrefab, transform.position, transform.rotation);
    }
}
