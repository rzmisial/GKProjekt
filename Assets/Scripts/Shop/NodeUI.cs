using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class representing the node contextual UI.
/// </summary>
public class NodeUI : MonoBehaviour
{
    /// <summary>
    /// UI object reference
    /// </summary>
    public GameObject ui;

    /// <summary>
    /// node to which the UI will belong
    /// </summary>
    private Node _target;

    /// <summary>
    /// reference to the cost textbox
    /// </summary>
    public Text upgradeCost;
    /// <summary>
    /// reference to the upgrade button
    /// </summary>
    public Button upgradeButton;

    /// <summary>
    /// reference to the selling value textbox
    /// </summary>
    public Text sellAmount;

    /// <summary>
    /// Method setting the chosen node as the target for the UI.
    /// </summary>
    /// <param name="node"></param>
    public void SetTarget(Node node)
    {
        _target = node;

        transform.position = _target.GetBuildPosition();

        if (!_target.isUpgraded)
        {
            upgradeCost.text = "$" + _target.turretBlueprint.UpgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + _target.turretBlueprint.GetSellAmount();


        ui.SetActive(true);
    }

    /// <summary>
    /// Method hiding the UI.
    /// </summary>
    public void Hide()
    {
        ui.SetActive(false);
    }

    /// <summary>
    /// Method handlig upgrade action chosen by the user.
    /// </summary>
    public void Upgrade()
    {
        _target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    /// <summary>
    /// Method handlig selling action chosen by the user.
    /// </summary>
    public void Sell()
    {
        _target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
