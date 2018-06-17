using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {


    BuildManager buildManager;

    // types of turret
    public TurretBluePrint basicTurret;
    public TurretBluePrint canon;
    public TurretBluePrint laserTurret;


    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectBasicTurret()
    {
        buildManager.SelectTurretToBuild(basicTurret);
    }

    public void SelectCanon()
    {
        buildManager.SelectTurretToBuild(canon);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
