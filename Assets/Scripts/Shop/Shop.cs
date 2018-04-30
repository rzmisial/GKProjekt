using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {


    BuildManager buildManager;

    // types of turret
    public TurretBluePrint basicTurret;
    

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectBasicTurret()
    {
        buildManager.SelectTurretToBuild(basicTurret);
    }
}
