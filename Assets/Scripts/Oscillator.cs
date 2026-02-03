using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 movementVector;

    Vector3 startPosition;
    Vector3 endPosition;
    float moveFactor;
    
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }
    void Update()
    {
        moveFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, moveFactor);
    }
}
