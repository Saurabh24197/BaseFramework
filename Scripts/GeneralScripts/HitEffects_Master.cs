using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects_Master : MonoBehaviour
{
    [TextArea]
    public string scriptInfo = "A Stub which holds all the Custom HitEffects for all types of Items/NPC/Destructibles." +
        "Which is passed onto the Gun.";
    public GameObject hitEffects;
    public float heWaitTime = 10f;

    public AudioClip hitAudio;
    public float hitVolume = 1.0f;
}
