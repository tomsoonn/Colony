﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public static UnitsManager me;
    public List<GameObject> myHouses;
    public List<GameObject> units;
    public int populationLimit;
    public int currentPoplualtion;

    float timer = 1.0f;

    void Awake()
    {
        me = this;
        myHouses = new List<GameObject>();
    }

    public void AddHouse(GameObject house)
    {
        myHouses.Add(house);
        CalculateMaxPopulation();
    }

    public void RemoveHouse(GameObject house)
    {
        myHouses.Remove(house);
        CalculateMaxPopulation();
    }

    public void AddUnit(GameObject g)
    {
        units.Add(g);
    }

    public void RemoveUnit(GameObject g)
    {
        units.Remove(g);
    }

    void CalculateMaxPopulation()
    {
        int popMax = myHouses.Count * 2;
        populationLimit = popMax;
    }

    void CalculateCurrentPopulation()
    {
        int popCount = 0;
        popCount += units.Count;
        currentPoplualtion = popCount;
    }

    public bool CanWeConstructUnit()
    {
        return populationLimit >= currentPoplualtion;
    }

    void Update()
    {
        MoniterPopulation();
    }

    void MoniterPopulation()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CalculateMaxPopulation();
            CalculateCurrentPopulation();
            timer = 1.0f;
        }
    }

    float originalWidth = 1920.0f; //scaleing will make the gui proportional to what you see onscreen no matter the resolution, will cause stretching
    float originalHeight = 1080.0f;
    Vector3 scale;
    float dispWidth = 1920.0f / 5;
    void OnGUI()
    {
        GUI.depth = 0;
        scale.x = Screen.width / originalWidth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;
        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        Rect pos = new Rect(dispWidth * 4, 0, dispWidth, 100);
        GUI.Box(pos, "Units: " + currentPoplualtion + "/" + populationLimit);

        GUI.matrix = svMat;
    }
}
