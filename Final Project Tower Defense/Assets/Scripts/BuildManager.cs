using UnityEngine;

[System.Serializable]
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;
}

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public TowerBlueprint arrowTower;
    public TowerBlueprint iceTower;
    public TowerBlueprint fireTower;
    public TowerBlueprint cannonTower;

    private TowerBlueprint towerToBuild;

    void Awake() { Instance = this; }

    public bool CanBuild => towerToBuild != null;

    public bool HasMoney => GameManager.Instance.gold >= towerToBuild.cost;

    public void SetTowerToBuild(string towerName)
    {
        TowerBlueprint selected = null;

        // Translate the string from the button into an actual blueprint
        if (towerName == "Arrow") selected = arrowTower;
        else if (towerName == "Ice") selected = iceTower;
        else if (towerName == "Fire") selected = fireTower;
        else if (towerName == "Cannon") selected = cannonTower;

        // TOGGLE LOGIC: If clicking the same button twice, deselect.
        if (towerToBuild == selected)
        {
            towerToBuild = null;
            Debug.Log("Deselected tower.");
        }
        else
        {
            towerToBuild = selected;
            Debug.Log("Selected: " + towerName);
        }
    }

    public TowerBlueprint GetTowerToBuild() => towerToBuild;
}