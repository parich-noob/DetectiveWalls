using UnityEngine;

public class HologramController : MonoBehaviour
{
    [Header("Float")]
    public float floatSpeed = 2f;

    public float floatHeight = 0.01f;

    [Header("Rotation")]
    public float rotationSpeed = 30f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.localPosition =
            startPos + Vector3.up * newY;

        transform.Rotate(Vector3.up,
            rotationSpeed * Time.deltaTime,
            Space.Self);
    }
}