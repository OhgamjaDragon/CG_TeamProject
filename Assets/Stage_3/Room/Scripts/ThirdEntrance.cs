using UnityEngine;

public class ThirdEntrance : MonoBehaviour
{
    
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
