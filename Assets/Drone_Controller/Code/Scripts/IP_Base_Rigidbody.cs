﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IPBase
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class IP_Base_Rigidbody : MonoBehaviour
    {

        #region Variables
        [Header("Rigidbody Properties")]
        [SerializeField] private float weightInLbs = 1f;
        [SerializeField] protected float startDrag;
        [SerializeField] protected float startAngularDrag;

        const float lbsToKg = 0.454f;

        protected Rigidbody rb;
        #endregion

        #region Main Methods

        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (rb)
            {
                rb.mass = weightInLbs * lbsToKg;
                startDrag = rb.drag;
                startAngularDrag = rb.angularDrag;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!rb)
            {
                return;
            }

            HandlePhysics();
        }
        #endregion


        #region Custom methods

        protected virtual void HandlePhysics()
        {

        }

        #endregion
    }
}