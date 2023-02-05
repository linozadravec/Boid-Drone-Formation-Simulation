using DroneController;
using Drones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FormationCheck : MonoBehaviour {


    DroneMovement[] boids;
    void Start()
    {
        boids = FindObjectsOfType<DroneMovement>();
    }

    void Update () {
        if (boids != null) {

            bool uFormaciji = true;
            foreach(DroneMovement b in boids)
            {
                if (!b.jeKraj)
                {
                    uFormaciji = false;
                }
            }

            if (uFormaciji)
            {
                foreach (DroneMovement b in boids)
                {
                    b.GetComponent<IP_Drone_Inputs>().enabled = true;
                    b.GetComponent<IP_Drone_Controller>().enabled = true;
                    b.GetComponent<Rigidbody>().useGravity = true;
                    b.GetComponent<PlayerInput>().enabled = true;
                }
            }
        }
    }
}