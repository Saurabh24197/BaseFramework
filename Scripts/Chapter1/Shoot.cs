using UnityEngine;
using System.Collections;

namespace Chapter1
{


    public class Shoot : MonoBehaviour
    {
        private float fireRate = 0.3f;
        private float nextFire;
        private RaycastHit hit;
        private float range = 300;


        private Transform myTransform;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();

        }


        void SetInitialReferences()
        {
            myTransform = transform;
        }
        // Update is called once per frame
        void Update()
        {
            CheckForInput();
        }

        void CheckForInput()
        {

            

            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {

                Debug.DrawRay(myTransform.TransformPoint(0,0,1), myTransform.forward, Color.cyan, 2);
              
                
                    if (Physics.Raycast(myTransform.TransformPoint(0, 0, 1), myTransform.forward, out hit, range))
                    {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        Debug.Log(hit.transform.name + " Attack of Enemy tags! :P");
                    }   
                    else Debug.Log(hit.transform.name + " Not Enemy!");

                }


                nextFire = Time.time + fireRate;
                //Debug.Log("Key Pressed with GetButton!");
            }
        }
    }
}
