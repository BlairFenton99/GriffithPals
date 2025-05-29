
using NUnit.Framework;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ClickManager : MonoBehaviour
{
    //references
    public TextMeshProUGUI hint1;
    public TextMeshProUGUI hint2;
    public TextMeshProUGUI resultText;
    public Button riddleSolvedButton;
    private bool laptopClicked = false;
    private bool laptop1Clicked = false;

    void Start()
    {
        // Hide resultText on start
        resultText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // check for clicks or touches
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // check if laptop clicked then update hint with text
                if (clickedObject.name == "Laptop")
                {
                    hint1.text = "I’m where aromas fill the air," +
                                "\r\nWith tasty treats and meals to share." +
                                "\r\nYour stomach growls, you’re on the way —" +
                                "\r\nWhere the coffee grind scent steals the day.";
                    laptopClicked = true;

                    // Hide resultText when laptops are clicked again
                    resultText.gameObject.SetActive(false);
                }
                // check if laptop1 clicked then update hint with text
                else if (clickedObject.name == "Laptop1")
                {
                    hint2.text = "By morning light, I come alive," +
                                "\r\nWith buzzing crowds who work and strive." +
                                "\r\nBut when the sun has gone away," +
                                "\r\nI’m quiet — till the break of day.";
                    laptop1Clicked = true;

                    // Hide resultText when laptop1 is clicked again
                    resultText.gameObject.SetActive(false);
                }
                else if (clickedObject.name == "ghostToy")
                {
                    //show result text
                    resultText.gameObject.SetActive(true); 

                    //choose which resultText to display depending on if all laptops are clicked
                    if (laptopClicked && laptop1Clicked)
                    {
                        //if all laptops are clicked, show the continue button
                        resultText.text = "From the sounds of those riddles, the only place I can think of would be the quad near the  Junction!";
                        riddleSolvedButton.gameObject.SetActive(true);

                    }
                    else
                    {
                        resultText.text = "The computers might hold some clues for what your looking for, come back and see me when you have " +
                            "checked them!";
                    }
                    
                }
            }
        }
    }

    //function for continue button to send to next scene
    public void OnRiddleSolvedButtonClick()
    {
        Debug.Log("Loading next level: level_06_finalbattle");
        SceneManager.LoadScene("level_06_finalbattle");
    }
}
