using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Formacija
{
    FOI,
    Krug,
    Linija
}

public class ProslijedivanjeKrozScenu : MonoBehaviour
{

    public int BrojDronova { get; set; }
    public Formacija Formacija { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
