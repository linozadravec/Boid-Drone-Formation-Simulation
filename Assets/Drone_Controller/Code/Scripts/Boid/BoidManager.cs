﻿using DroneController;
using Drones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoidManager : MonoBehaviour {

    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;
    Boid[] boids;

    //dodano
    //public Transform target;

    void Start()
    {
        boids = FindObjectsOfType<Boid>();
        //foreach (Boid b in boids)
        //{
        //    //target je bil null
        //    b.Initialize(settings, target);
        //}

    }

    void Update () {
        if (boids != null) {

            bool uFormaciji = true;
            foreach(Boid b in boids)
            {
                if (!b.jeKraj)
                {
                    uFormaciji = false;
                }
            }

            if (uFormaciji)
            {
                //IP_Drone_Inputs droneInputsScript;
                //droneInputsScript = GetComponent<IP_Drone_Inputs>();

                foreach (Boid b in boids)
                {
                    b.GetComponent<IP_Drone_Inputs>().enabled = true;
                    b.GetComponent<IP_Drone_Controller>().enabled = true;
                    b.GetComponent<Rigidbody>().useGravity = true;
                    b.GetComponent<PlayerInput>().enabled = true;
                }
            }

            int numBoids = boids.Length;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < boids.Length; i++) {
                boidData[i].position = boids[i].position;
                boidData[i].direction = boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer (numBoids, BoidData.Size);
            boidBuffer.SetData (boidData);

            compute.SetBuffer (0, "boids", boidBuffer);
            compute.SetInt ("numBoids", boids.Length);
            compute.SetFloat ("viewRadius", settings.perceptionRadius);
            compute.SetFloat ("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt (numBoids / (float) threadGroupSize);
            compute.Dispatch (0, threadGroups, 1, 1);

            boidBuffer.GetData (boidData);

            for (int i = 0; i < boids.Length; i++) {
                boids[i].avgFlockHeading = boidData[i].flockHeading;
                boids[i].centreOfFlockmates = boidData[i].flockCentre;
                boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

                boids[i].UpdateBoid ();
            }

            boidBuffer.Release ();
        }
    }

    public struct BoidData {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size {
            get {
                return sizeof (float) * 3 * 5 + sizeof (int);
            }
        }
    }
}