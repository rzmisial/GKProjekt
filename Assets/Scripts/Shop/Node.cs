using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Turret;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;

/// <summary>
/// Class representing a single node on which turrets can be built.
/// </summary>
public class Node : MonoBehaviour {

    /// <summary>
    /// color of the node on hover
    /// </summary>
    public Color HoverColor;
    /// <summary>
    /// color of the node on hover when the player doesn't have enough money
    /// </summary>
    public Color NotEnoughMoneyColor;

    /// <summary>
    /// offset for turret building
    /// </summary>
    public Vector3 positionOffset;

    /// <summary>
    /// currently built turret
    /// </summary>
    [HideInInspector]
    public GameObject Turret;

    /// <summary>
    /// turret to built scheme
    /// </summary>
    [HideInInspector]
    public TurretBluePrint turretBlueprint;

    /// <summary>
    /// information on whether or not the turret is upgraded
    /// </summary>
    [HideInInspector]
    public bool isUpgraded = false;

    /// <summary>
    /// Neutral color of the node
    /// </summary>
    private Color nativeColor;
    
    /// <summary>
    /// renderer instance used fot changing colour
    /// </summary>
    private Renderer rend;

    /// <summary>
    /// reference to the build manager
    /// </summary>
    BuildManager buildManager;

    /// <summary>
    /// Initializer method.
    /// </summary>
    private void Start()
    {
        buildManager = BuildManager.Instance;

        rend = GetComponent<Renderer>();
        nativeColor = rend.material.color;
    }

    /// <summary>
    /// Obtains inforomation on where to build the new turret.
    /// </summary>
    /// <returns>position of the new building spot</returns>
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    /// <summary>
    /// Handler for the mouse click event - opens the contextual UI and selects the node.
    /// </summary>
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

    /// <summary>
    /// Handler for the mouse pointing event - changes the node color.
    /// </summary>
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

    /// <summary>
    /// Handler for the mouse not pointing event - changes the node color back to neutral.
    /// </summary>
    private void OnMouseExit()
    {
        rend.material.color = nativeColor;
    }

    /// <summary>
    /// Creates a new turret.
    /// </summary>
    /// <param name="blueprint">scheme of the turret to build</param>
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

    /// <summary>
    /// Handles turret upgrading.
    /// </summary>
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

    /// <summary>
    /// Handles turret selling.
    /// </summary>
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
