using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public BoidSettings settings;
    public Transform prefab;
    public Transform dronovi;
    public Transform ciljevi;
    public int razmakDronovaLinija = 4;
    public int razmakDronovaKruznica = 4;

    int brojDronova;
    float inkrementStupnjeva;

    public static List<Transform> slobodniCiljevi = new List<Transform>();

    void Awake()
    {
        ProslijedivanjeKrozScenu proslijedivanjeKrozScenu = FindObjectOfType<ProslijedivanjeKrozScenu>();

        brojDronova = proslijedivanjeKrozScenu.BrojDronova;
       
        switch (proslijedivanjeKrozScenu.Formacija)
        {
            case Formacija.FOI:
                InstanciranjeRandom(FormacijaFOI.TRANSFORM_LIST_FOI_X.Count);
                ZadajFormacijuFOI();
                break;
            case Formacija.Krug:
                InstanciranjeRandom(FormacijaKrug.TRANSFORM_LIST_KRUG_X.Count);
                ZadajFormacijuKruznica();
                break;
            case Formacija.Linija:
                InstanciranjeRandom(FormacijaLinija.TRANSFORM_LIST_LINIJA_X.Count);
                ZadajFormacijuLinija();
                break;
            default:
                Debug.Log("Nema formacije");
                break;
        }
    }

    private void InstanciranjeRandom(int brojDronova)
    {
        List<float> xCoordinates = new List<float>();
        List<float> zCoordinates = new List<float>();
        float xRandom;
        float zRandom;
        bool ispravno = true;

        for (int i = 0; i < brojDronova; i++)
        {
            do
            {
                ispravno = true;
                xRandom = UnityEngine.Random.Range(-brojDronova/3, brojDronova/3);
                zRandom = UnityEngine.Random.Range(-brojDronova/3, brojDronova/3);

                for (int j = 0; j < xCoordinates.Count; j++)
                {
                    if (Math.Abs(xCoordinates[j] - xRandom) < 0.6 && Math.Abs(zCoordinates[j] - zRandom) < 0.6)
                    {
                        ispravno = false;
                    }
                }

            } while (!ispravno);

            xCoordinates.Add(xRandom);
            zCoordinates.Add(zRandom);

            Vector3 spawnPosition;
            spawnPosition.x = xRandom;
            spawnPosition.y = -4f;
            spawnPosition.z = zRandom;


            Instantiate(prefab, spawnPosition, Quaternion.identity, dronovi);
        }
    }

    private void ZadajFormacijuFOI()
    {
        DroneMovement[] boids;
        boids = FindObjectsOfType<DroneMovement>();

        for (int i = 0; i < FormacijaFOI.TRANSFORM_LIST_FOI_X.Count; i++)
        {
            GameObject cilj = new GameObject("Cilj" + i);
            Vector3 ciljPosition;
            ciljPosition.x = FormacijaFOI.TRANSFORM_LIST_FOI_X[i];
            ciljPosition.y = FormacijaFOI.TRANSFORM_LIST_FOI_Y[i];
            ciljPosition.z = FormacijaFOI.TRANSFORM_LIST_FOI_Z[i];
            cilj.transform.position = ciljPosition;
            cilj.transform.parent = ciljevi;

            slobodniCiljevi.Add(cilj.transform);

        }

        foreach(DroneMovement boid in boids)
        {
            boid.Odluci();
        }
    }


    private void ZadajFormacijuKruznica()
    {

        DroneMovement[] boids;
        boids = FindObjectsOfType<DroneMovement>();

        for (int i = 0; i < FormacijaKrug.TRANSFORM_LIST_KRUG_X.Count; i++)
        {
            GameObject cilj = new GameObject("Cilj" + i);
            Vector3 ciljPosition;
            ciljPosition.x = FormacijaKrug.TRANSFORM_LIST_KRUG_X[i];
            ciljPosition.y = FormacijaKrug.TRANSFORM_LIST_KRUG_Y[i];
            ciljPosition.z = FormacijaKrug.TRANSFORM_LIST_KRUG_Z[i];
            cilj.transform.position = ciljPosition;
            cilj.transform.parent = ciljevi;

            slobodniCiljevi.Add(cilj.transform);
        }

        foreach (DroneMovement boid in boids)
        {
            boid.Odluci();
        }
    }

    private void ZadajFormacijuLinija()
    {
        DroneMovement[] boids;
        boids = FindObjectsOfType<DroneMovement>();

        for (int i = 0; i < FormacijaLinija.TRANSFORM_LIST_LINIJA_X.Count; i++)
        {
            GameObject cilj = new GameObject("Cilj" + i);
            Vector3 ciljPosition;
            ciljPosition.x = FormacijaLinija.TRANSFORM_LIST_LINIJA_X[i];
            ciljPosition.y = FormacijaLinija.TRANSFORM_LIST_LINIJA_Y[i];
            ciljPosition.z = FormacijaLinija.TRANSFORM_LIST_LINIJA_Z[i];
            cilj.transform.position = ciljPosition;
            cilj.transform.parent = ciljevi;

            slobodniCiljevi.Add(cilj.transform);
        }

        foreach (DroneMovement boid in boids)
        {
            boid.Odluci();
        }

    }
}
