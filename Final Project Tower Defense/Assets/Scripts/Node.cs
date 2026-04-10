using UnityEngine;
using UnityEngine.InputSystem;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.green;
    public Color notPlaceableColor = Color.red;
    public LayerMask nodeLayer;

    private GameObject tower;
    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
     
        startColor = rend.material.color;
    }

    void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Vector2 mousePosition = Pointer.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, nodeLayer))
        {
            if (hit.transform == transform)
            {
                BuildTowerOnNode();
            }
        }
    }

    void BuildTowerOnNode()
    {
        if (!BuildManager.Instance.CanBuild || tower != null) return;
        if (!BuildManager.Instance.HasMoney) return;

        TowerBlueprint blueprint = BuildManager.Instance.GetTowerToBuild();
        GameManager.Instance.gold -= blueprint.cost;
        tower = Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
    }

    void OnMouseEnter()
    {
        if (!BuildManager.Instance.CanBuild) return;
        rend.material.color = (tower != null) ? notPlaceableColor : hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}