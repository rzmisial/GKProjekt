using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Turret;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;

public class Node : MonoBehaviour {

    public Color HoverColor;
    public Color NotEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject Turret;
    [HideInInspector]
    public TurretBluePrint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

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

        if (Turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
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


    void BuildTurret(TurretBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.Cost)
        {
            return;
        }

        PlayerStats.Money -= blueprint.Cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.Prefab, GetBuildPosition(), Quaternion.identity);
        Turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

    }

    public void UpgradeTurret()
    {
        IUpgrading turret = Turret.GetComponent(typeof(IUpgrading)) as IUpgrading;

        if (PlayerStats.Money < turretBlueprint.UpgradeCost)
        {
            return;
        }

        if (turret == null)
        {
            return;
        }

        PlayerStats.Money -= turretBlueprint.UpgradeCost;
        
        turret.Upgrade();  

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        // The same effect as build turret in Unity Inspector
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(Turret);
        turretBlueprint = null;
    }
}
