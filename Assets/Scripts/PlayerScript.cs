using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    public float flatStrength;
    public LogicScript logic;
    public bool PlayerIsAlive = true;
    private bool isColliding = false;
    private int count = 0;

    // Different Player Looks
    public GameObject AlivePlayer;
    public GameObject AlivePlayer_Scared;
    public GameObject AlivePlayer_Happy;
    public GameObject AlivePlayer_Sunglasses;
    public GameObject DeadPlayer;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) == true || Input.touchCount > 0) && PlayerIsAlive == true)
        {
            myrigidbody.velocity = Vector2.up * flatStrength;
        }
        isColliding = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerIsAlive == true)
        {
            logic.gameOver();
            PlayerIsAlive = false;
        }
        // Kill all Faces
        AlivePlayer.SetActive(false);
        AlivePlayer_Scared.SetActive(false);
        AlivePlayer_Happy.SetActive(false);
        AlivePlayer_Sunglasses.SetActive(false);
        // Die
        DeadPlayer.SetActive(true);
    }
    public void FaceChange()
    {
        int NewFace = Random.Range(1, 3);

        if (PlayerIsAlive == true && NewFace == 1) {
            AlivePlayer.SetActive(false);
            AlivePlayer_Sunglasses.SetActive(true);
            StartCoroutine(SleepFunction());
            //ReturnFace();
        }
        if (PlayerIsAlive == true && NewFace == 2)
        {
            AlivePlayer.SetActive(false);
            AlivePlayer_Scared.SetActive(true);
            StartCoroutine(SleepFunction());
            //ReturnFace();
        }
        if (PlayerIsAlive == true && NewFace == 3)
        {
            AlivePlayer.SetActive(false);
            AlivePlayer_Happy.SetActive(true);
            StartCoroutine(SleepFunction());
            //ReturnFace();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        count++;

        if (collision.gameObject.layer == 1 && PlayerIsAlive == true && count%2 == 0)
        {
            //Debug.Log("Triggered: " + count);
            FaceChange();
        }
    }

    public void ReturnFace()
    {

        AlivePlayer_Scared.SetActive(false);
        AlivePlayer_Happy.SetActive(false);
        AlivePlayer_Sunglasses.SetActive(false);
        // Default
        AlivePlayer.SetActive(true);
    }

    IEnumerator SleepFunction()
    {
        yield return new WaitForSeconds(2);
        ReturnFace();
    }

}
