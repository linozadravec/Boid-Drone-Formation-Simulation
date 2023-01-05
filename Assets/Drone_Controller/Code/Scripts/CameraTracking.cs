using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    
    public Transform kamera;

    public float udaljenostOdKamere;

    public TextMeshProUGUI xKoordinataText;
    public TextMeshProUGUI yKoordinataText;
    public TextMeshProUGUI zKoordinataText;
    public TextMeshProUGUI brzinaText;

    Vector3 centar;
    Vector3 staraPozicija = Vector3.zero;
    float speed;
    int brojDronova;



    void Update()
    {
        kamera.position = IzracunajCentar() + new Vector3(0, 0, udaljenostOdKamere * -1);
        UpdateGUI();
    }

    public Vector3 IzracunajCentar()
    {
        Vector3 zbrojVektora = new Vector3();
        brojDronova = 0;

        foreach (Transform dron in transform)
        {
            zbrojVektora += dron.position;
            brojDronova++;
        }
        centar = zbrojVektora / brojDronova;

        return centar;
    }

    private void UpdateGUI()
    {
        xKoordinataText.text = "X: " + centar.x.ToString("F2");
        yKoordinataText.text = "Y: " + centar.y.ToString("F2");
        zKoordinataText.text = "Z: " + centar.z.ToString("F2");
    }

    void FixedUpdate()
    {
        speed = (kamera.position - staraPozicija).magnitude;
        speed *= 100;
        staraPozicija = kamera.position;
        brzinaText.text = "Brzina: " + speed.ToString("F2");

    }
}
