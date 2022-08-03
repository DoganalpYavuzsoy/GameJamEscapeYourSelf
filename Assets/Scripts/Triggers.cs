using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Triggers : MonoBehaviour
{
    public Text Trigger;

    void Start()
    {
        Trigger.text = "D��manlara Dikkat Et!";
    }

   void OnTriggerEnter(Collider collider)
    {
        if (this.gameObject.tag == "Trigger1")
        {
            Trigger.text = "�lmeden Kale'den ��k��� Bul.";
        }
        
        if (this.gameObject.tag == "Trigger2")
        {
            Trigger.text = "B�l�m� Bitirmek ��in Hediyenize gidin.";
        }

        if (this.gameObject.tag == "Trigger3")
        {
            Trigger.text = "GAME OVER!!";
        }
    }
}
