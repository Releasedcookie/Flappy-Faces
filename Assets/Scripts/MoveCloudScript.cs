using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloudScript : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float deadZone = -45;
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * MoveSpeed) * Time.deltaTime;
        MoveSpeed = logic.MoveSpeed;


        if (transform.position.x < deadZone)
        {
            // Debug.Log("Cloud Deleted");
            Destroy(gameObject);
        }

    }
}
