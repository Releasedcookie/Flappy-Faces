using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceFlyingScript : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    public float flatStrength;
    public LogicScript logic;
    public bool FaceIsAlive = true;
    // public TextMeshProUGUI EmojiFace;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
       // EmojiFace.text = "😀";
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) == true || Input.touchCount > 0) && FaceIsAlive == true)
        {
            myrigidbody.velocity = Vector2.up * flatStrength;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        FaceIsAlive = false;

       // EmojiFace.text = "😵";

    }
}
