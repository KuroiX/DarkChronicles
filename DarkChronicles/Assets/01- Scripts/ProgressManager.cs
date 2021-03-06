﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public enum ProgressState
    {
        Start = 0,
        CutScene1 = 1,
        CutScene2 = 2,
        CutScene3 = 3,
        CutScene4 = 4,
        CutScene5 = 5,
        CutScene6 = 6,
    }

    public ProgressState state = ProgressState.Start;
    public static ProgressManager Manager;

    [SerializeField] private GameObject[] gOs;

    public bool missile;
    public bool shockwave;
    public bool shrink;

    public void EnableMissile()
    {
        missile = true;
    }

    public void EnableMissileAndShrink()
    {
        missile = true;
        shrink = true;
    }

    public void DisableAbilites()
    {
        missile = false;
        shockwave = false;
        shrink = false;
    }
    

    void Start()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnVillageLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// This function sets up the village depending on the progress of the game.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnVillageLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            cutscenes = GameObject.Find("Cut Scenes");
            switch (state)
            {
                case ProgressState.Start:
                    break;
                case ProgressState.CutScene1:
                    // put lizard in house
                    GameObject.Find("Lizard").transform.position = new Vector3(-4.25f, 11.5f, 0);
                    // enable cutscene 2
                    DestroyCutscenes((int)state, true);
                    break;
                case ProgressState.CutScene2:
                    // put lizard in house
                    GameObject.Find("Lizard").transform.position = new Vector3(-4.25f, 11.5f, 0);
                    // enable cutscene 2
                    DestroyCutscenes((int)state, false);
                    break;
                case ProgressState.CutScene3:
                    // put father & box there
                    GameObject.Find("Father").transform.position = new Vector3(15, -4.5f, 0);
                    GameObject.Find("TheBox").transform.position = new Vector3(14, -4.5f, 1.57f);
                    DestroyCutscenes((int) state, true);
                    break;
                case ProgressState.CutScene4:
                    GameObject.Find("Father").transform.position = new Vector3(-5.5f, 10.75f, 0);
                    GameObject.Find("TheBox").transform.position = new Vector3(14, -4.5f, 1.57f);
                    DestroyCutscenes((int) state, true);
                    break;
                case ProgressState.CutScene5:
                    // remove bullies
                    GameObject.Find("Sylvain").transform.position = new Vector3(-30, 11, 0);
                    GameObject.Find("Kruralt").transform.position = new Vector3(-30, 10, 0);
                    GameObject.Find("Nalo").transform.position = new Vector3(-30, 9, 0);
                    DestroyCutscenes((int) state, true);
                    
                    GameObject.Find("Father").transform.position = new Vector3(-5.5f, 10.75f, 0);
                    GameObject.Find("TheBox").transform.position = new Vector3(14, -4.5f, 1.57f);
                    GameObject liz = GameObject.Find("Lizard");
                    liz.transform.position = new Vector3(-10.75f, -3, 0);
                    liz.GetComponent<SpriteRenderer>().flipX = true;
                    // remove villagers?
                    for (int i = 1; i <= 5; i++)
                    {
                        GameObject gO = GameObject.Find("npc_" + i);
                        Destroy(gO);
                    }
                    // maybe let some ask questions
                    foreach (GameObject gO in gOs)
                    {
                        Instantiate(gO);
                    }

                    break;
                case ProgressState.CutScene6:
                    // who knows
                    break;
            }
        }
    }

    public GameObject cutscenes;

    public void EndCutScene(int index)
    {
        //Debug.Log("haha: " + index);
        state = (ProgressState)index;
    }

    /// <summary>
    /// This function destroys all the cutscene objects that the player has seen before. 
    /// </summary>
    /// <param name="index">Destroys all cutscenes until index</param>
    /// <param name="active">Sets the cutscene at index to active</param>
    void DestroyCutscenes(int index, bool active)
    {
        Debug.Log("ProgressManager: DestroyCutscenes() " + index);

        Transform parent = cutscenes.transform;
        
        for (int i = 0; i <= index && i < parent.childCount; i++)
        {
            if (i != index)
            {
                
                parent.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                parent.GetChild(i).gameObject.SetActive(active);
            }
        }
    }

    /*private bool pressed;
    void Update()
    {
        if (!pressed && Input.anyKeyDown)
        {
            pressed = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }*/
}
