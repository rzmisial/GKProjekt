using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for managing building new turrets.
/// </summary>
public class BuildManager : MonoBehaviour {

    /// <summary>
    /// reference to the default turret object
    /// </summary>
    public GameObject standardTurret;

    /// <summary>
    /// reference to a build manager instance
    /// </summary>
    public static BuildManager Instance;

    /// <summary>
    /// scheme of the new turret
    /// </summary>
    private TurretBluePrint turretToBuild;
    /// <summary>
    /// reference to the currently sellected node
    /// </summary>
    private Node _selectedNode;

    /// <summary>
    /// reference to the contextual node interface
    /// </summary>
    public NodeUI NodeUi;

    /// <summary>
    /// reference to the building visual effect
    /// </summary>
    public GameObject buildEffect;

    /// <summary>
    /// reference to the selling visual effect
    /// </summary>
    public GameObject sellEffect;

    /// <summary>
    /// Initializer method - first stage.
    /// </summary>
    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;    
    }

    /// <summary>
    /// Initializer method - second stage.
    /// </summary>
    private void Start()
    {
        turretToBuild = null;
    }

    /// <summary>
    /// Method handling new node selection by player.
    /// </summary>
    /// <param name="node">node to select</param>
    public void SelectNode(Node node)
    {
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }

        _selectedNode = node;
        turretToBuild = null;
        NodeUi.SetTarget(node);
    }

    /// <summary>
    /// Handles selecting a new turret to build.
    /// </summary>
    /// <param name="turret">scheme of the new turret</param>
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    /// <summary>
    /// Method informing about whether or not building is possible.
    /// </summary>
    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    /// <summary>
    /// Method informing about whether or not the player has enough money to build.
    /// </summary>
    public bool HasMoney
    {
        get { return PlayerStats.Money >=turretToBuild.Cost ; }
    }

    /// <summary>
    /// Method obtaining the new turret to build.
    /// </summary>
    /// <returns>new turret to build</returns>
    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    /// <summary>
    /// Method responsible for hiding the contextual UI when the node is deselcted.
    /// </summary>
    public void DeselectNode()
    {
        NodeUi.Hide();
        _selectedNode = null;
    }
}
