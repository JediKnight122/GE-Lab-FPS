using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f;
    void Update()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);
    }
}
