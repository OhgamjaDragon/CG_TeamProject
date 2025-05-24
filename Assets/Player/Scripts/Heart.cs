using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject heartUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public void TakeDamage(int damage)
    {
        HeartUIManager uIManager = heartUI.GetComponent<HeartUIManager>();

        if (uIManager != null)
        {
            uIManager.TakeDamage(damage);
        }
    }

    public void Heal(int heal)
    {
        HeartUIManager uIManager = heartUI.GetComponent<HeartUIManager>();

        if (uIManager != null)
        {
            uIManager.Heal(heal);
        }
    }
}
