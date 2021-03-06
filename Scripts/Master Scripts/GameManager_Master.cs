﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BaseFramework
{
    public class GameManager_Master : MonoBehaviour
    {

        public delegate void GameManagerEventHandler();

        public event GameManagerEventHandler MenuToggleEvent;
        public event GameManagerEventHandler InventoryUIToggleEvent;
        public event GameManagerEventHandler RestartLevelEvent;
        public event GameManagerEventHandler GoToMenuSceneEvent;
        public event GameManagerEventHandler GameOverEvent;

        public bool isGameOver;
        public bool isInventoryUIOn;
        public bool isMenuOn;

        [Header("UIGO's to Toggle, on Menu/Inventory UI show.")]
        public List<GameObject> ToggleGameObjects;


        public void ToggleUIGameObjects(bool state)
        {
            foreach (GameObject go in ToggleGameObjects)
            {
                go.SetActive(state);
            }
        }

        public void CallEventMenuToggle()
        {
            if (MenuToggleEvent != null)
            {
                MenuToggleEvent();
            }
        }

        public void CallEventInventoryUIToggle()
        {
            if (InventoryUIToggleEvent != null)
            {
                InventoryUIToggleEvent();
            }
        }

        public void CallEventRestartLevel()
        {
            if (RestartLevelEvent != null)
            {
                RestartLevelEvent();
            }
        }

        public void CallEventGoToMenuScene()
        {
            if (GoToMenuSceneEvent != null)
            {
                GoToMenuSceneEvent();
            }
        }

        public void CallEventGameOver()
        {
            if (GameOverEvent != null)
            {
                if (!isGameOver)
                {
                    isGameOver = true;
                    GameOverEvent();
                }
            }
        }


    }
}

