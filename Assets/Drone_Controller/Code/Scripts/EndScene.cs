using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public Canvas krajCanvas;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI endText;

    public CameraTracking cameraTracking;

    ProslijedivanjeKrozScenu proslijedivanjeKrozScenu;

    Vector3 centar;

    private void Start()
    {
        proslijedivanjeKrozScenu = FindObjectOfType<ProslijedivanjeKrozScenu>();
        krajCanvas.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            krajCanvas.enabled = true;

            startText.text = "Start koordinate \n" + "X: 0 \tY: 0 \tZ: 0";

            

            centar = cameraTracking.IzracunajCentar();

            endText.text = "End koordinate \n " + "X: " + centar.x.ToString("F2") + " \tY: " + centar.y.ToString("F2") + " \tZ: " + centar.z.ToString("F2") + " ";
        }
    }

    public void ReloadScene()
    {
        Destroy(proslijedivanjeKrozScenu.gameObject);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    
}
