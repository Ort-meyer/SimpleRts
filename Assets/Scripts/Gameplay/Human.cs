using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Human : MonoBehaviour
{
    [Serializable]
    public class HumanConfigData
    {
        public Texture2D selectionTexture;
        public Texture2D selectionEdgeTexture;
        public float edgeThickness;
    }

    [Serializable]
    public class HumanStateData
    {
        public bool isDragging = false;
    }

    [Serializable]
    public class HumanBookkeepData
    {
        public Vector3 mouseDownPoint;
        Dictionary<int, BaseUnit> hitUnits;

        public HumanBookkeepData()
        {
            hitUnits = new Dictionary<int, BaseUnit>();
        }
    }

    public HumanConfigData m_configData;
    private HumanStateData m_stateData;
    private HumanBookkeepData m_bookData;


    private Player m_player;
    // Use this for initialization
    void Start()
    {
        m_player = GetComponent<Player>();
        InputManager.Instance.M_RegisterInputCallbackReleased(KeyCode.Mouse1, KeyModifier.None, M_MoveOrAttackOrder);
        InputManager.Instance.M_RegisterInputCallbackReleased(KeyCode.Mouse0, KeyModifier.None, M_SelectUnits);
        InputManager.Instance.M_RegisterInputCallbackPressed(KeyCode.Mouse0, M_StartDragging);
        

        m_stateData = new HumanStateData();
        m_bookData = new HumanBookkeepData();
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
            GameObject closestObjHit = hits[0].transform.gameObject;
            BaseUnit hitUnit = closestObjHit.GetComponentInParent<BaseUnit>();
            // Hit other player unit (all other players are enemies for now)
            if (hitUnit && hitUnit.m_configData.faction != m_player.m_configData.faction)
            {
                m_player.M_AttackOrder(closestObjHit.transform);
            }
            else
            {
                m_player.M_MoveOrder(hits[0].point);
            }
        }
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
                    if(hitUnit.m_configData.faction == m_player.m_configData.faction)
                    {
                        hitUnits[hitUnit.GetInstanceID()] = hitUnit;
                    }
                }
            }
        }
        // Selection box
        if (m_stateData.isDragging)
        {
            foreach (var kvp in m_player.M_GetAllUnits())
            {
                if (IsWithinSelectionBounds(kvp.Value.gameObject))
                {
                    hitUnits[kvp.Key] = kvp.Value;
                }
            }
        }
        m_stateData.isDragging = false;
        m_player.M_SelectUnits(hitUnits.Values.ToList()); // Can be improved
    }

    private void M_StartDragging()
    {
        m_stateData.isDragging = true;
        m_bookData.mouseDownPoint = Input.mousePosition;
    }

    /////// Selection box stuff
    private void OnGUI()
    {
        if (m_stateData.isDragging)
        {
            Rect rect = GetScreenRect(m_bookData.mouseDownPoint, Input.mousePosition);
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
        GUI.DrawTexture(rect, m_configData.selectionTexture);
        // Draw the edges
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, rect.width, m_configData.edgeThickness), m_configData.selectionEdgeTexture);
        // Left
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, m_configData.edgeThickness, rect.height), m_configData.selectionEdgeTexture);
        // Right
        GUI.DrawTexture(new Rect(rect.xMax - m_configData.edgeThickness, rect.yMin, m_configData.edgeThickness, rect.height), m_configData.selectionEdgeTexture);
        // Bottom
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMax - m_configData.edgeThickness, rect.width, m_configData.edgeThickness), m_configData.selectionEdgeTexture);
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
        var viewportBounds = GetViewportBounds(m_bookData.mouseDownPoint, Input.mousePosition);
        return viewportBounds.Contains(Camera.main.WorldToViewportPoint(gameObject.transform.position));
    }

}
