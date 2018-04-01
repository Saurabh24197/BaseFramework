using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Melee_Master : MonoBehaviour
    {

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventPlayerInput;
        public event GeneralEventHandler EventMeleeReset;

        public delegate void MeleeHitEventHandler(Collision hitCol, Transform hitTransform);
        public event MeleeHitEventHandler EventHit;

        public bool isInUse = false;
        public float swingRate = .5f;

        public void CallEventPlayerInput()
        {
            if (EventPlayerInput != null)
            {
                EventPlayerInput();
            }
        }

        public void CallEventMeleeReset()
        {
            if (EventMeleeReset != null)
            {
                EventMeleeReset();
            }
        }

        public void CallEventHit(Collision hCol, Transform hTransform)
        {
            if (EventHit != null)
            {
                EventHit(hCol, hTransform);
            }
        }
    }
}

