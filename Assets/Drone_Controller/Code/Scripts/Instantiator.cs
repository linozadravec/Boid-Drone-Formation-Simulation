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
                Debug.Log("Jos nije implementirano");
                break;
            case Formacija.Linija:
                Debug.Log("Jos nije implementirano");
                break;
            default:
                Debug.Log("Nema formacije");
                break;
        }

        //if (proslijedivanjeKrozScenu.Formacija == Formacija.Krug)
        //{
        //    InstanciranjeRandom();
        //    //InstanciranjeKruznica();
        //}
        //else
        //{
        //    InstanciranjeLinija();
        //}
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
                xRandom = UnityEngine.Random.Range(-5f, 5f);
                zRandom = UnityEngine.Random.Range(-14f, 6f);

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
        Boid[] boids;
        boids = FindObjectsOfType<Boid>();

        for (int i = 0; i < FormacijaFOI.TRANSFORM_LIST_FOI_X.Count; i++)
        {
            GameObject cilj = new GameObject("Cilj" + i);
            Vector3 ciljPosition;
            ciljPosition.x = FormacijaFOI.TRANSFORM_LIST_FOI_X[i];
            ciljPosition.y = FormacijaFOI.TRANSFORM_LIST_FOI_Y[i];
            ciljPosition.z = FormacijaFOI.TRANSFORM_LIST_FOI_Z[i];
            cilj.transform.position = ciljPosition;
            cilj.transform.parent = ciljevi;

            boids[i].Initialize(settings, cilj.transform);
            //Debug.Log(cilj.name + ": "+ cilj.transform.position.x +" "+ cilj.transform.position.y +" " + cilj.transform.position.z);
            //Instantiate(cilj, ciljPosition, Quaternion.identity, ciljevi);
        }
    }


    private void ZadajFormacijuKruznica()
    {
        //for(int i = 0; i < FormacijaFOI.TRANSFORM_LIST_FOI_X.Count; i++)
        //{
        //    Vector3 spawnPosition;
        //    spawnPosition.x = FormacijaFOI.TRANSFORM_LIST_FOI_X[i];
        //    spawnPosition.y = FormacijaFOI.TRANSFORM_LIST_FOI_Y[i];
        //    spawnPosition.z = FormacijaFOI.TRANSFORM_LIST_FOI_Z[i];

        //    Instantiate(prefab, spawnPosition, Quaternion.identity, dronovi);
        //}

        Boid[] boids;
        boids = FindObjectsOfType<Boid>();

        for (int i = 0; i < FormacijaKrug.TRANSFORM_LIST_KRUG_X.Count; i++)
        {
            GameObject cilj = new GameObject("Cilj" + i);
            Vector3 ciljPosition;
            ciljPosition.x = FormacijaKrug.TRANSFORM_LIST_KRUG_X[i];
            ciljPosition.y = FormacijaKrug.TRANSFORM_LIST_KRUG_Y[i];
            ciljPosition.z = FormacijaKrug.TRANSFORM_LIST_KRUG_Z[i];
            cilj.transform.position = ciljPosition;
            cilj.transform.parent = ciljevi;

            boids[i].Initialize(settings, cilj.transform);
            //Debug.Log(cilj.name + ": "+ cilj.transform.position.x +" "+ cilj.transform.position.y +" " + cilj.transform.position.z);
            //Instantiate(cilj, ciljPosition, Quaternion.identity, ciljevi);
        }

        //inkrementStupnjeva = 360 / 10;
        //inkrementStupnjeva = Mathf.Round(inkrementStupnjeva * 100.0f) * 0.01f + 0.5f;

        //for (float i = 0; i < 360; i += inkrementStupnjeva)
        //{
        //    Vector3 spawnPosition;
        //    float angle = i * Mathf.Deg2Rad;

        //    spawnPosition.x = (razmakDronovaKruznica * Mathf.Cos(angle));
        //    spawnPosition.y = (razmakDronovaKruznica * Mathf.Sin(angle));
        //    spawnPosition.z = 0;

        //    Debug.Log(spawnPosition.x + " " + spawnPosition.y + " " + spawnPosition.z);

        //    Instantiate(prefab, spawnPosition, Quaternion.identity, dronovi);

        //}
    }

    private void InstanciranjeLinija()
    {
        if (brojDronova % 2 == 0)
        {
            for (int i = 0; i < brojDronova; i++)
            {
                int x = razmakDronovaLinija / 2 + (i / 2 * razmakDronovaLinija);

                if (i % 2 == 0)
                {
                    Instantiate(prefab, new Vector3(x * -1, 0, 0), Quaternion.identity, dronovi);
                }
                else
                {
                    Instantiate(prefab, new Vector3(x, 0, 0), Quaternion.identity, dronovi);
                }
            }
        }
        else
        {
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, dronovi);

            for (int i = 0; i < brojDronova - 1; i++)
            {
                int x = razmakDronovaLinija + (i / 2 * razmakDronovaLinija);

                if (i % 2 == 0)
                {
                    Instantiate(prefab, new Vector3(x * -1, 0, 0), Quaternion.identity, dronovi);
                }
                else
                {
                    Instantiate(prefab, new Vector3(x, 0, 0), Quaternion.identity, dronovi);
                }
            }
        }
    }
}
