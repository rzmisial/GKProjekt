using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public GameObject standardTurret;

    public static BuildManager Instance;

    private GameObject turretToBuild;

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;    
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    private void Start()
    {
        turretToBuild = null;
    }

    public void SetTurretToBuild (GameObject turret)
    {
        turretToBuild = turret;
    }
}
