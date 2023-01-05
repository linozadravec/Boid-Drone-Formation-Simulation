using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OdabirFormacije : MonoBehaviour
{
    public ProslijedivanjeKrozScenu proslijedivanjeKrozScenu;
    public TextMeshProUGUI linijaText;
    public TextMeshProUGUI krugText;
    public TextMeshProUGUI foiText;
    void Start()
    {
        proslijedivanjeKrozScenu.Formacija = Formacija.Krug;
        linijaText.enabled = false;
        foiText.enabled = false;
        krugText.enabled = true;
    }

    public void OdaberiKrug()
    {
        proslijedivanjeKrozScenu.Formacija = Formacija.Krug;
        krugText.enabled = true;
        linijaText.enabled = false;
        foiText.enabled = false;

    }
    public void OdaberiFOI()
    {
        proslijedivanjeKrozScenu.Formacija = Formacija.FOI;
        foiText.enabled = true;
        linijaText.enabled = false;
        krugText.enabled = false;

    }

    public void OdaberiLiniju()
    {
        proslijedivanjeKrozScenu.Formacija = Formacija.Linija;
        linijaText.enabled = true;
        krugText.enabled = false;  
        foiText.enabled = false;
    }

}
