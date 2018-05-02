using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public GameObject standardTurret;

    public static BuildManager Instance;

    private TurretBluePrint turretToBuild;
    private Node _selectedNode;
    public NodeUI NodeUi;

    public GameObject buildEffect;
    public GameObject sellEffect;

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;    
    }

    private void Start()
    {
        turretToBuild = null;
    }

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

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public bool HasMoney
    {
        get { return PlayerStats.Money >=turretToBuild.Cost ; }
    }


    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void DeselectNode()
    {
        NodeUi.Hide();
        _selectedNode = null;
    }
}
