using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePage_CloudMoves : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float deadZone = -45;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position + (Vector3.left * MoveSpeed) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * MoveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            // Debug.Log("Cloud Deleted");
            Destroy(gameObject);
        }

    }
}
