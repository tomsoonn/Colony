using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager me;
    public int food = 0, wood = 0, stone = 0, gold = 0;

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

    public void ReduceResources(string resource, int amount)
    {
        ChangeResourceAmount(resource, amount * -1);
    }

    public void IncreaseResources(string resource, int amount)
    {
        ChangeResourceAmount(resource, amount);
    }

    public bool CheckResourceAmount(string resource, int amount)
    {
        switch (resource)
        {
            case "food":
                if (food >= amount)
                {
                    return true;
                } else
                {
                    return false;
                }
            case "wood":
                if (wood >= amount)
                {
                    return true;
                } else
                {
                    return false;
                }
            case "stone":
                if (stone >= amount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "gold":
                if (gold >= amount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                break;
        }
        return false;
    }

    public void ChangeResourceAmount(Resource res, int amount)
    {
        var @switch = new Dictionary<Type, System.Action>
        {
            {typeof(Food), () => { food += amount; }},
            {typeof(Gold), () => { gold += amount; }},
            {typeof(Wood), () => { wood += amount; }},
            {typeof(Stone), () => { stone += amount; }},
        };
        @switch[res.GetType()]();
    }
    
    public void ChangeResourceAmount(string resource, int amount)
    {
        switch (resource)
        {
            case "food":
                food += amount;
                break;
            case "wood":
                wood += amount;
                break;
            case "stone":
                stone += amount;
                break;
            case "gold":
                gold += amount;
                break;
            default:
                break;
        }
    }

    public bool CanWeBulid(Building ba)
    {
        return (CheckResourceAmount("wood", ba.woodCost)
            && CheckResourceAmount("food", ba.foodCost)
            && CheckResourceAmount("stone", ba.stoneCost)
            && CheckResourceAmount("gold", ba.goldCost));
    }

    public bool CanWeBuildUnit(Unit u)
    {
        return (CheckResourceAmount("wood", u.woodCost)
           && CheckResourceAmount("food", u.foodCost)
           && CheckResourceAmount("stone", u.stoneCost)
           && CheckResourceAmount("gold", u.goldCost));
    }

    public void BuildBuilding(Building toBuild)
    {
        ReduceResources("wood", toBuild.woodCost);
        ReduceResources("food", toBuild.foodCost);
        ReduceResources("stone", toBuild.stoneCost);
        ReduceResources("gold", toBuild.goldCost);
    }

    public void BuildUnit(Unit toBuild)
    {
        ReduceResources("wood", toBuild.woodCost);
        ReduceResources("food", toBuild.foodCost);
        ReduceResources("stone", toBuild.stoneCost);
        ReduceResources("gold", toBuild.goldCost);
    }

    float originalWidth = 1920.0f;
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
        GUI.skin.box.fontSize = 50;

        for (int x =0; x<4; x++)
        {
            Rect pos = new Rect(0 + (dispWidth * x), 0, dispWidth, 100);

            switch (x)
            {
                case 0:
                    GUI.Box(pos, "Food: " + food.ToString());
                    break;
                case 1:
                    GUI.Box(pos, "Wood: " + wood.ToString());
                    break;
                case 2:
                    GUI.Box(pos, "Stone: " + stone.ToString());
                    break;
                case 3:
                    GUI.Box(pos, "Gold: " + gold.ToString());
                    break;
                default:
                    break;
            }

        }
        GUI.matrix = svMat;

    }

}
