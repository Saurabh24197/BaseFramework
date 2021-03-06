﻿using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class ThrowGrenade : MonoBehaviour
    {
        public GameObject grenadePrefab;
        private Transform myTransform;
        public float propulsionForce;
        public AudioSource audioGrenadeShoot;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SpawnGrenade();
            }
        }

        void SetInitialReferences()
        {
            myTransform = transform;
        }
        void SpawnGrenade()
        {
            GameObject go = (GameObject)Instantiate(grenadePrefab, myTransform.TransformPoint(0.3f, -0.2f, 0.5f), myTransform.rotation);
            go.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.Impulse);
            Destroy(go, 5f);
            audioGrenadeShoot.Play();

        }
    }
}

