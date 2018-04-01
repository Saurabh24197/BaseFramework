using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProceduralCave
{
    [System.Serializable]
    public struct SpawnStructure
    {
        public string name;
        public bool nearPlayer;
        public bool randomizeGO;

		[Tooltip("Delete the Entry after first Instanciation.")]
		public bool deleteEntry;

        [Space]
        [Header("List of GameObjects [<NPC>, <Item>, <Weapon>..]")]
        public GameObject[] go_List;
    }

    [System.Serializable]
    public struct SpawnAbsolutes
    {
        //Deploy All [GameObject]'s in All Locations.
        public string name;
		public bool onlyPlayerPos;
		public bool ignorePlayerPos;


        [Tooltip("[Vector(x, GO.pos - coord.wallHeight + 0.1f, z)]")]
        public bool useWallHeight;
        public Vector3 posVector;

        [Space]
        [Header("List of GameObjects [<NPC>, <Item>, <Weapon>..]")]
        public GameObject[] go_List;
    }
}

