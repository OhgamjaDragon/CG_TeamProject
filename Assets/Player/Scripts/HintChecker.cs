using UnityEngine;

public class HintChecker : MonoBehaviour
{
    public GameObject hintManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HintUIManager uIManager = hintManager.GetComponent<HintUIManager>();
            if (uIManager != null) {
                uIManager.ShowHintText();
            }
        }
    }
}
