using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color HoverColor;
    public Color NotEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject Turret;

    private Color nativeColor;
    
    private Renderer rend;

    BuildManager buildManager;

    


    private void Start()
    {
        buildManager = BuildManager.Instance;

        rend = GetComponent<Renderer>();
        nativeColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (Turret != null)
        {
            return;
        }

        buildManager.BuildTurretOn(this);
    }
    
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = HoverColor;
        }
        else
        {
            rend.material.color = NotEnoughMoneyColor;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = nativeColor;
    }
}
