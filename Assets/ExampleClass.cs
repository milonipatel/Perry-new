using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    public float speed = 10f;


    void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
      //  transform.Rotate(Vector3.down, speed * Time.deltaTime);
    }
}