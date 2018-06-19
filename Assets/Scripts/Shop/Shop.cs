using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Method representing the shop - buing a new turret.
/// </summary>
public class Shop : MonoBehaviour {

    /// <summary>
    /// reference to the build manager.
    /// </summary>
    BuildManager buildManager;

    // types of turrets

    /// <summary>
    /// basic turret scheme
    /// </summary>
    public TurretBluePrint basicTurret;

    /// <summary>
    /// canon scheme
    /// </summary>
    public TurretBluePrint canon;

    /// <summary>
    /// laser turret scheme
    /// </summary>
    public TurretBluePrint laserTurret;

    /// <summary>
    /// Initializer method
    /// </summary>
    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    /// <summary>
    /// Method handling basic turret sellection
    /// </summary>
    public void SelectBasicTurret()
    {
        buildManager.SelectTurretToBuild(basicTurret);
    }

    /// <summary>
    /// Method handling canon sellection
    /// </summary>
    public void SelectCanon()
    {
        buildManager.SelectTurretToBuild(canon);
    }

    /// <summary>
    /// Method handling laser turret sellection
    /// </summary>
    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
