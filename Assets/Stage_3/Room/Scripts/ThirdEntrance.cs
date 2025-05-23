using UnityEngine;

public class ThirdEntrance : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null) {
                pm.ReadyNoneGravityRoom();
            }
        }
    }
}
