using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // speed of movement

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right;
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
