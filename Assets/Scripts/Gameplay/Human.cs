using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Human : MonoBehaviour
{
    // Config
    public Texture2D m_selectionTexture;
    public Texture2D m_selectionEdgeTexture;
    public float m_edgeThickness;

    // State
    private bool m_isDragging = false;
    private Vector3 m_mouseDownPoint;
    private Dictionary<int, BaseUnit> m_hitUnits = new Dictionary<int, BaseUnit>();


    private Player m_player;
    // Use this for initialization
    void Start()
    {
        m_player = GetComponent<Player>();
        InputManager.Instance.M_RegisterInputCallbackReleased(KeyCode.Mouse1, KeyModifier.None, M_MoveOrAttackOrder);
        InputManager.Instance.M_RegisterInputCallbackReleased(KeyCode.Mouse0, KeyModifier.None, M_SelectUnits);
        InputManager.Instance.M_RegisterInputCallbackPressed(KeyCode.Mouse0, M_StartDragging);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void M_MoveOrAttackOrder()
    {
        RaycastHit[] hits = InputManager.Instance.M_GetMousePointerHits();
        if (hits.Length > 0)
        {
            GameObject closestObjHit = M_ClosestHit(hits).transform.gameObject;
            BaseUnit hitUnit = closestObjHit.GetComponentInParent<BaseUnit>();
            // Hit other player unit (all other players are enemies for now)
            if (hitUnit && hitUnit.m_faction != m_player.m_faction)
            {
                m_player.M_AttackOrder(closestObjHit.transform);
            }
            else
            {
                m_player.M_MoveOrder(hits[0].point);
            }
        }
    }

    private RaycastHit M_ClosestHit(RaycastHit[] hits)
    {
        float closest = 1000000;
        RaycastHit closestHit = new RaycastHit();
        foreach(RaycastHit hit in hits)
        {
            if(hit.distance < closest)
            {
                closest = hit.distance;
                closestHit = hit;
            }
        }
        return closestHit;
    }
    
    private void M_SelectUnits()
    {
        m_player.M_ClearSelection();
        // Key is ID, val is unit
        Dictionary<int, BaseUnit> hitUnits = new Dictionary<int, BaseUnit>();
        // Single click
        RaycastHit[] hits = InputManager.Instance.M_GetMousePointerHits();
        if (hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                BaseUnit hitUnit = hit.transform.GetComponentInParent<BaseUnit>();
                if (hitUnit)
                {
                    if(hitUnit.m_faction == m_player.m_faction)
                    {
                        hitUnits[hitUnit.GetInstanceID()] = hitUnit;
                    }
                }
            }
        }
        // Selection box
        if (m_isDragging)
        {
            foreach (var kvp in m_player.M_GetAllUnits())
            {
                if (IsWithinSelectionBounds(kvp.Value.gameObject))
                {
                    hitUnits[kvp.Key] = kvp.Value;
                }
            }
        }
        m_isDragging = false;
        m_player.M_SelectUnits(hitUnits.Values.ToList()); // Can be improved
    }

    private void M_StartDragging()
    {
        m_isDragging = true;
        m_mouseDownPoint = Input.mousePosition;
    }

    /////// Selection box stuff
    private void OnGUI()
    {
        if (m_isDragging)
        {
            Rect rect = GetScreenRect(m_mouseDownPoint, Input.mousePosition);
            DrawSelectionBox(rect);
        }
    }

    private Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    private void DrawSelectionBox(Rect rect)
    {
        // Draw the inner box
        GUI.DrawTexture(rect, m_selectionTexture);
        // Draw the edges
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, rect.width, m_edgeThickness), m_selectionEdgeTexture);
        // Left
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, m_edgeThickness, rect.height), m_selectionEdgeTexture);
        // Right
        GUI.DrawTexture(new Rect(rect.xMax - m_edgeThickness, rect.yMin, m_edgeThickness, rect.height), m_selectionEdgeTexture);
        // Bottom
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMax - m_edgeThickness, rect.width, m_edgeThickness), m_selectionEdgeTexture);
    }

    private Bounds GetViewportBounds(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = Camera.main.nearClipPlane;
        max.z = Camera.main.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        var viewportBounds = GetViewportBounds(m_mouseDownPoint, Input.mousePosition);
        return viewportBounds.Contains(Camera.main.WorldToViewportPoint(gameObject.transform.position));
    }

}
