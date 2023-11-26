using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RememberName : MonoBehaviour
{

    [SerializeField] private TMP_InputField inputField;


    // Start is called before the first frame update
    private void Start()
    {

        string nameTextBox = "";

        if (PlayerPrefs.HasKey("PlayerID"))
        {
            nameTextBox = PlayerPrefs.GetString("PlayerID");
            inputField.text = nameTextBox;

            Debug.Log("Setting Name to " + nameTextBox);
        }


    }

}
