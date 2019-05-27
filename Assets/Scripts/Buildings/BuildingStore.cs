using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BuildingStore : MonoBehaviour
{
    public static BuildingStore me;
    public GameObject[] buildings;
    [SerializeField]
    GameObject selectedBuilding;

    public List<Building> buildingsInScene;

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

    public GameObject getToBuild()
    {
        return selectedBuilding;
    }

    public void cleanSelectedBuilding()
    {
        selectedBuilding = null;
    }

    void OnGUI()
    {
		int yMod = 0;
		foreach (GameObject b in buildings) {
				
			try {
				Building buildingScr = b.GetComponent<Building> ();
				Rect pos = new Rect (50, 50 + (50 * yMod), 100, 50);
				if (GUI.Button (pos,buildingScr.name)) {
					selectedBuilding = b;
                    PanelController.me.Clean();
				}
				yMod+=1;
			} catch {
				Debug.Log ("Building missing a component");
			}
		}
    }

    public void CreatingBuilding(Vector2 vector2)
    {
        GameObject selectedBuilding = BuildingStore.me.getToBuild();
        if (selectedBuilding != null)
        {
            Building selectedBuildScript = selectedBuilding.GetComponent<Building>();
            if (selectedBuildScript != null)
            {
                if (ResourceManager.me.canWeBulid(selectedBuildScript))
                {
                    Vector3 spawnPos = new Vector3(vector2.x, vector2.y, 0);
                    spawnPos.z = 0;
                    GameObject built = (GameObject)Instantiate(BuildingStore.me.getToBuild(), spawnPos, Quaternion.Euler(0, 0, 0));
                    SpriteRenderer sr = built.AddComponent<SpriteRenderer>();
                    sr.sprite = built.GetComponent<Building>().buildingSprite;
                    sr.sortingOrder = 10;
                    built.AddComponent<BoxCollider2D>();
                    built.SetActive(true);
                    buildingsInScene.Add(built.GetComponent<Building>());
                    ResourceManager.me.buildBuilding(selectedBuildScript);
                    if (built.GetComponent<Building>().name.Equals("House"))
                    {
                        UnitsManager.me.addHouse(built);
                    }
                }
                else
                {
                    //TODO Not enough resources warning
                }
            }
        }
        cleanSelectedBuilding();

    }
}
