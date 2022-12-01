using UnityEngine;

public class RotateMarker : MonoBehaviour
{
    float speed = 75f;
    void Update() => transform.Rotate(Vector3.up * speed * Time.deltaTime);
}
