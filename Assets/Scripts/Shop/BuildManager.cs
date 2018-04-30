using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public GameObject standardTurret;

    public static BuildManager Instance;

    private TurretBluePrint turretToBuild;

    public GameObject buildEffect;

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

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }

    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public bool HasMoney
    {
        get { return PlayerStats.Money >=turretToBuild.cost ; }
    }

    public void BuildTurretOn(Node node)
    {

        if (PlayerStats.Money < turretToBuild.cost)
        {
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret =  Instantiate(this.turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);

        node.Turret = turret;

        GameObject effect =  Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);

        Destroy(effect, 2f);
    }
}
