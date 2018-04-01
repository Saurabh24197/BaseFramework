using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class LaserColor : MonoBehaviour
    {

        private Color laserColor = new Color(0.7f, 0f, 0f, 1f);

        private float laserRedCounter = 1f;

        //public Color grenadeColor = new Color(0.7f, 0f, 0f, 1f);

        //private float grenadeRedCounter = 0.7f;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {

                laserRedCounter -= 0.05f;

                if (laserRedCounter < 0)
                {

                    laserRedCounter = 1f;
                }

                laserColor = new Color(laserRedCounter, 0f, 0f, 1f);
                GetComponent<Renderer>().material.SetColor("_Color", laserColor);
            }


        }
    }
}

