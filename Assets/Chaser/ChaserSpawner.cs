using UnityEngine;

public class ChaserSpawner : MonoBehaviour
{

    public GameObject chaserPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool chaserExists;

    private void Start()
    {
        chaserExists = false;
    }
    public void SpawnChaser()
    {
        if (!chaserExists) {
            Instantiate(chaserPrefab, transform.position, transform.rotation);
            chaserExists = true;
        }
    }
}
