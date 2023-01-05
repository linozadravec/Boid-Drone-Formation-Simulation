using Drones;
using Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DroneEngine
{
    [RequireComponent(typeof(BoxCollider))]

    public class IP_Drone_Engine : MonoBehaviour, IEngine
    {
        #region Variables

        [Header("Engine Properties")]
        [SerializeField] private float maxPower = 4f;

        #endregion


        #region Interface Methods

        public void InitEngine()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEngine(Rigidbody rb, IP_Drone_Inputs input)
        {
            //Debug.Log("Running engine" + gameObject.name);

            Vector3 engineForce = Vector3.zero;
            engineForce = transform.up * ((rb.mass * Physics.gravity.magnitude) + (input.Throttle * maxPower))/4f;

            rb.AddForce(engineForce, ForceMode.Force);
        }

        #endregion
    }
}