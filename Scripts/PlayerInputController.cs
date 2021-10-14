using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    private bool  accelerate, brake;
    public float speed = 1f;
    public GameObject CAR;
    public Text text1;


    public void FixedUpdate()
    {
        //Pokud je aktivni tlacitko plynu

        if (accelerate)
        {
            Debug.Log("ACCELERATE");
            CAR.GetComponent<CarController>().verticalInput = speed;
        }
        else
        {

            CAR.GetComponent<CarController>().verticalInput = 0f;
        }


        //Pokud je aktivni tlacitko brzdy
        if (brake)
        {
            Debug.Log("breaking");
            CAR.GetComponent<CarController>().isBreaking = true;

        }else
        {
           
            CAR.GetComponent<CarController>().isBreaking = false;

        }
    }


        //Ovladani radici paky

    public void HandleShiftLever()
    {

        if (text1.text == "1")
        {
            text1.text = "2";
            speed = 2f;
        }
        else if (text1.text == "2")
        {
            text1.text = "R";
            speed = -1f;
        }
        else if (text1.text == "R")
        {
            text1.text = "1";
            speed = 1f;
        }


    }


    //Odpovedi na zpravu z unity o stisknuti klavesy(tlacitka)

    public void OnAccelerate(InputValue inputValue) => accelerate = inputValue.isPressed;
    public void OnBrake(InputValue inputValue) => brake = inputValue.isPressed;

    public void OnAccelerate1(InputValue inputValue) => accelerate = inputValue.isPressed;
}






