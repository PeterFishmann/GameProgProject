using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetRotation = target.transform.eulerAngles;
        float yRotation = targetRotation.y;
        Vector3 cameraRotation = new Vector3(50f, yRotation, 0f);

        transform.position = new Vector3(target.position.x, target.position.y + 10, target.position.z - 6);
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
