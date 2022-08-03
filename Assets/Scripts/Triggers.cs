using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Triggers : MonoBehaviour
{
    public Text Trigger;

    void Start()
    {
        Trigger.text = "Düþmanlara Dikkat Et!";
    }

   void OnTriggerEnter(Collider collider)
    {
        if (this.gameObject.tag == "Trigger1")
        {
            Trigger.text = "Ölmeden Kale'den Çýkýþý Bul.";
        }
        
        if (this.gameObject.tag == "Trigger2")
        {
            Trigger.text = "Bölümü Bitirmek Ýçin Hediyenize gidin.";
        }

        if (this.gameObject.tag == "Trigger3")
        {
            Trigger.text = "GAME OVER!!";
        }
    }
}
