using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BuildingStore : MonoBehaviour
{
    public static BuildingStore me;
    public GameObject[] buildings;
    public GameObject[] units;
    [SerializeField]
    GameObject selectedToBuild;

    public List<Building> buildingsInScene;

    float timer = 0.1f; //delay for setting selectedToBuild

    string warning = "";

    void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetToBuild()
    {
        return selectedToBuild;
    }

    public void CleanSelected()
    {
        selectedToBuild = null;
        StartCoroutine(CleanWarning());
    }

    IEnumerator CleanWarning()
    {
        yield return new WaitForSeconds(2.0f);
        warning = "";
    }

    IEnumerator Select(GameObject g)
    {
        yield return new WaitForSeconds(timer);
        selectedToBuild = g;
    }

    public void CreateSelected(Vector2 vector2)
    {
        GameObject selectedBuilding = GetToBuild();
        if (selectedBuilding != null)
        {
            Building selectedBuildScript = selectedBuilding.GetComponent<Building>();
            if (selectedBuildScript != null)
            {
                if (ResourceManager.me.CanWeBulid(selectedBuildScript))
                {
                    Vector3 spawnPos = new Vector3(vector2.x, vector2.y, 0);
                    GameObject built = (GameObject)Instantiate(GetToBuild(), spawnPos, Quaternion.Euler(0, 0, 0));
                    SpriteRenderer sr = built.AddComponent<SpriteRenderer>();
                    sr.sprite = built.GetComponent<Building>().buildingSprite;
                    sr.sortingOrder = 10;
                    built.AddComponent<BoxCollider2D>();
                    built.SetActive(true);
                    buildingsInScene.Add(built.GetComponent<Building>());
                    ResourceManager.me.BuildBuilding(selectedBuildScript);
                    if (built.GetComponent<Building>().name.Equals("House"))
                    {
                        UnitsManager.me.AddHouse(built);
                    }
                    Debug.Log("Building builded");
                }
                else
                {
                    //TODO Not enough resources warning
                    warning = "Not enough resources";
                    Debug.Log("Not enough resources");  
                }
            }
            Unit selectedUnitScript = selectedBuilding.GetComponent<Unit>();
            if (selectedUnitScript != null)
            {
                if (ResourceManager.me.CanWeBuildUnit(selectedUnitScript) && UnitsManager.me.canWeConstructUnit())
                {
                    Vector3 spawnPos = new Vector3(vector2.x, vector2.y, 0);
                    GameObject built = (GameObject)Instantiate(GetToBuild(), spawnPos, Quaternion.Euler(0, 0, 0));
                    //SpriteRenderer sr = built.AddComponent<SpriteRenderer>();
                    //sr.sprite = built.GetComponent<Unit>().buildingSprite;
                    //sr.sortingOrder = 10;
                    //built.AddComponent<BoxCollider2D>();
                    built.SetActive(true);
                    UnitsManager.me.addUnit(built);
                    ResourceManager.me.BuildUnit(selectedUnitScript);
                  
                    Debug.Log("Unit created");
                }
                else if (!UnitsManager.me.canWeConstructUnit())
                {
                    warning = "Population limit reached";
                    Debug.Log("Population limit reached");
                }
                else
                {
                    warning = "Not enough resources";
                    Debug.Log("Not enough resources");
                }
            }
        }
        CleanSelected();

    }

    void OnGUI()
    {
        int yMod = 0;
        foreach (GameObject b in buildings)
        {

            try
            {
                Building buildingScr = b.GetComponent<Building>();
                Rect pos = new Rect(10, 50 + (30 * yMod), 70, 30);

                if (GUI.Button(pos, buildingScr.name))
                {
                    CleanSelected();
                    StartCoroutine(Select(b));
                    PanelController.me.Clean();
                }
                yMod += 1;
            }
            catch
            {
                Debug.Log("Building missing a component");
            }
        }
        foreach (GameObject u in units)
        {

            try
            {
                Unit unitScr = u.GetComponent<Unit>();
                Rect pos = new Rect(10, 50 + (30 * yMod), 70, 30);
                if (GUI.Button(pos, unitScr.name))
                {
                    CleanSelected();
                    StartCoroutine(Select(u));
                    PanelController.me.Clean();
                }
                yMod += 1;
            }
            catch
            {
                Debug.Log("Unit missing a component");
            }
        }

        if (selectedToBuild)
        {
            Rect pos1 = new Rect(10, 50 + 30 * yMod, 70, 80);
            GUI.skin.box.fontSize = 12;
            Building selectedBuildScript = GetToBuild().GetComponent<Building>();
            if (selectedBuildScript != null)
            {
                GUI.Box(pos1, selectedBuildScript.CostString());
            }
            Unit selectedUnitScript = GetToBuild().GetComponent<Unit>();
            if (selectedUnitScript != null)
            {
                GUI.Box(pos1, selectedUnitScript.CostString());
            }
        }
        if (!String.IsNullOrEmpty(warning))
        {
            Rect pos1 = new Rect(10, 30, 150, 20);
            GUI.skin.box.fontSize = 12;
            GUI.Box(pos1, warning);
        }
        
    }
}
