using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromjenaBrojke : MonoBehaviour
{
    public ProslijedivanjeKrozScenu proslijedivanjeKrozScenu;
    public TextMeshProUGUI brojkaText;
    int brojDronova;

    void Start()
    {
        brojDronova = 2;
        brojkaText.text = brojDronova.ToString();
        proslijedivanjeKrozScenu.BrojDronova = 2;
    }

    public void Inkrementiraj()
    {
        if (brojDronova < 10)
        {
            brojDronova++;
            brojkaText.text = brojDronova.ToString();
            proslijedivanjeKrozScenu.BrojDronova = brojDronova;
        }
    }

    public void Dekrementiraj()
    {
        if (brojDronova > 2)
        {
            brojDronova--;
            brojkaText.text = brojDronova.ToString();
            proslijedivanjeKrozScenu.BrojDronova = brojDronova;
        }
    }
}