using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

namespace BaseFramework
{
    public enum AutomationType
    {
        Item_Default,
        Item_All,
        Item_Melee,
        Item_Gun,
        Item_Destructible,
        Destructible,
        NPC_Relational,

    }

    public class AutomateItem : MonoBehaviour
    {
        public bool automateSelf = false;

        public List<GameObject> goList;
        public AutomationType type;

        // Use this for initialization
        void Start()
        {
            if (automateSelf)
            {
                goList.Add(gameObject);
            }

            Debug.Log("[AutomateItem] Parent GameObject: " + gameObject.name + " Automation Type: " + type);
            AddComponents();
        }

        void AddComponents()
        {
            foreach (GameObject go in goList)
            {
                Debug.Log("[AutomateItem] GO_ListItem: " + go.name);
                go.name += " :Automated:";

                switch(type)
                {
                    case AutomationType.Item_Default:
                        {
                            DefaultItemComponents(go);
                            break;
                        }

                    case AutomationType.Item_All:
                        {
                            Debug.LogWarning("[AutomateItem] Automation Type: " + type + ". Remove any Unnecessary components later!");
        
                            DefaultItemComponents(go);
                            AdditionalItemComponents(go);
                            break;
                        }

                    case AutomationType.Item_Melee:
                        {
                            DefaultItemComponents(go);
                            MeleeComponenets(go);
                            break;
                        }

                    case AutomationType.Item_Gun:
                        {
                            DefaultItemComponents(go);
                            GunComponents(go);
                            break;
                        }

                    case AutomationType.Item_Destructible:
                        {
                            DefaultItemComponents(go);
                            DestructibleComponents(go);
                            break;
                        }

                    case AutomationType.Destructible:
                        {
                            DestructibleComponents(go);
                            break;
                        }

                    case AutomationType.NPC_Relational:
                        {
                            NPCComponents(go);
                            break;
                        }
                }


            }
        }

        void AdditionalItemComponents(GameObject go)
        {
            go.AddComponent<Item_Ammo>().enabled = false;
            go.AddComponent<Item_Medkit>().enabled = false;
            go.AddComponent<Item_ConvexMeshManagement>().enabled = false;
            go.AddComponent<Item_MakeNoise>().enabled = false;
        }

        void DefaultItemComponents(GameObject go)
        {
            //Item_Default has enough components for a basic functioning Item.
            go.AddComponent<Item_Master>();

            go.AddComponent<Item_Animator>();
            go.AddComponent<Item_Colliders>();
            go.AddComponent<Item_Rigidbodies>();

            go.AddComponent<Item_Pickup>();
            go.AddComponent<Item_Drop>();
            go.AddComponent<Item_Throw>();

            go.AddComponent<Item_Name>();
            go.AddComponent<Item_Tag>();
            go.AddComponent<Item_SetLayer>();

            go.AddComponent<Item_UI>();
            go.AddComponent<Item_SetTransform>();
            go.AddComponent<Item_Sounds>();
            go.AddComponent<Item_Transparency>();
        }

        void GunComponents(GameObject go)
        {
            go.AddComponent<Gun_Master>();

            go.AddComponent<Gun_StandardInput>();
            go.AddComponent<Gun_NPCInput>().enabled = false;

            go.AddComponent<Gun_Ammo>();
            go.AddComponent<Gun_Shoot>();
            go.AddComponent<Gun_ApplyDamage>();
            go.AddComponent<Gun_ApplyForce>();

            go.AddComponent<Gun_Animator>();
            go.AddComponent<Gun_AmmoUI>();
            go.AddComponent<Gun_BurstFireIndicator>();
            go.AddComponent<Gun_DynamicCrosshair>();
            go.AddComponent<Gun_MuzzleFlash>();
            go.AddComponent<Gun_HitEffects>();
            go.AddComponent<Gun_Sounds>();

            go.AddComponent<Gun_Reset>();
        }

        void MeleeComponenets(GameObject go)
        {
            go.AddComponent<Melee_Master>();

            go.AddComponent<Melee_StandardInput>();
            go.AddComponent<Melee_Swing>();
            go.AddComponent<Melee_Strike>();
            go.AddComponent<Melee_HitEffects>();
            go.AddComponent<Melee_Sounds>();
            go.AddComponent<Melee_Reset>();
        }

        void DestructibleComponents(GameObject go)
        {
            go.AddComponent<Destructible_Master>();

            go.AddComponent<Destructible_Health>();
            go.AddComponent<Destructible_CollisionDetection>();
            go.AddComponent<Destructible_TakeDamage>();
            go.AddComponent<Destructible_Degenerate>();
            go.AddComponent<Destructible_IntermediaryEffects>();

            go.AddComponent<Destructible_Explode>();
            go.AddComponent<Destructible_Sounds>();
            go.AddComponent<Destructible_ParticleSpawn>();
            go.AddComponent<Destructible_ActivateShards>();
            go.AddComponent<Destructible_CauseDistraction>();

            go.AddComponent<Destructible_Timer>().enabled = false;
            go.AddComponent<Destructible_PlayerInventoryUpdate>().enabled = false;
            go.AddComponent<Destructible_NPCRelation>().enabled = false;
        }

        void NPCComponents(GameObject go)
        {
            GameObject hitboxDefault = new GameObject("Hitbox");
            GameObject hitboxStruck = new GameObject("HitboxStruck");
            GameObject collissionField = new GameObject("CollissionField");
            GameObject stateFlag = new GameObject("StateFlag");

            if (go.GetComponent<Animator>() == null)
            {
                go.AddComponent<Animator>().enabled = false;
            }

            else go.GetComponent<Animator>().enabled = false;

            if (go.GetComponent<NavMeshAgent>() == null)
            {
                go.AddComponent<NavMeshAgent>();
            }

            if (go.GetComponent<AudioSource>() == null)
            {
                go.AddComponent<AudioSource>();
            }

            go.AddComponent<NPC_Master>();
            go.AddComponent<NPC_StatePattern>();
            go.AddComponent<NPC_Animations>();

            go.AddComponent<NPCTO_Animator>();
            go.AddComponent<NPCTO_StatePattern>();
            go.AddComponent<NPCTO_NavMeshAgent>();
            go.AddComponent<NPCTO_ChildGameObjects>();

            go.AddComponent<NPC_Health>();
            go.AddComponent<NPC_TakeDamage>();
            go.AddComponent<NPC_DropItems>();
            go.AddComponent<NPC_HeadLook>();

            go.AddComponent<NPC_SetMyAttacker>();
            go.AddComponent<HitEffects_Master>();

            go.AddComponent<NPC_ScoresUpdate>().enabled = false;
            go.AddComponent<NPC_ApplyRelations>().enabled = false;
            go.AddComponent<NPC_RagdollActivation>().enabled = false;
            go.AddComponent<NPC_OnHeadCollision>().enabled = false;
            go.AddComponent<NPC_HazardousSkin>().enabled = false;
            go.AddComponent<NPC_HoldRangedWeapons>().enabled = false;


            //Add Hitbox and it's Components to GameObject
            hitboxDefault.transform.SetParent(go.transform);
            hitboxDefault.AddComponent<BoxCollider>();
            hitboxDefault.AddComponent<Rigidbody>().isKinematic = true;
            hitboxDefault.GetComponent<Rigidbody>().useGravity = false;
            hitboxDefault.AddComponent<NPC_TakeDamage>();
            hitboxDefault.tag = go.tag;
            hitboxDefault.layer = go.layer;

            //Add Hitbox for Struck and it's Components to GameObject
            hitboxStruck.transform.SetParent(go.transform);
            hitboxStruck.AddComponent<BoxCollider>();
            hitboxStruck.AddComponent<Rigidbody>().isKinematic = true;
            hitboxStruck.GetComponent<Rigidbody>().useGravity = false;
            hitboxStruck.AddComponent<NPC_TakeDamage>();
            hitboxStruck.tag = go.tag;
            hitboxStruck.layer = go.layer;
            hitboxStruck.SetActive(false);

            //Add the CollisionField and it's Component
            collissionField.transform.SetParent(go.transform);
            collissionField.AddComponent<BoxCollider>();
            collissionField.GetComponent<BoxCollider>().isTrigger = true;
            collissionField.AddComponent<Rigidbody>().isKinematic = true;
            collissionField.GetComponent<Rigidbody>().useGravity = false;
            collissionField.AddComponent<NPC_CollisionField>();
            collissionField.tag = "Untagged";
            collissionField.layer = LayerMask.NameToLayer("Ignore Raycast");

            //Add the StateFlag to NPC GameObject.
            stateFlag.AddComponent<MeshFilter>();
            stateFlag.AddComponent<MeshRenderer>();
            stateFlag.transform.SetParent(go.transform);

            //Automatically Setup some variables
            go.GetComponent<NPC_Animations>().hitBoxDefault = hitboxDefault;
            go.GetComponent<NPC_Animations>().hitBoxStruck = hitboxStruck;
            go.GetComponent<NPC_StatePattern>().meshRendererFlag = stateFlag.GetComponent<MeshRenderer>();
            go.GetComponent<NPC_StatePattern>().npcMaster = go.GetComponent<NPC_Master>();

            Debug.LogWarning("Manually Setup the Ragdoll and Add NPC_Ragdoll Activation Script.\n Also add a GameObject as NPC's Head.");
        }
    }
}

