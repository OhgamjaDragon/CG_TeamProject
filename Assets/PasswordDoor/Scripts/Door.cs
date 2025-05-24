using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 1f; // 1이면 1초 동안 회전

    private Quaternion closedRotation;
    private Quaternion openedRotation;
    private float t = 0f; // 보간 비율 (0~1)

    void Start()
    {
        closedRotation = transform.rotation;
        openedRotation = Quaternion.Euler(0f, openAngle, 0f) * closedRotation;
    }

    void Update()
    {
        if (isOpen && t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(closedRotation, openedRotation, t);
        }
        else if (!isOpen && t > 0f)
        {
            t -= Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(closedRotation, openedRotation, t);
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }
}