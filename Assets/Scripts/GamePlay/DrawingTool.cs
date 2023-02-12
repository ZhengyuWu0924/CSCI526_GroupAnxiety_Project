using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ToolType { PEN = 0, BRUSH, NONE };


/// <summary>
/// Drawing Tool controls every pen and brush in the scene
/// </summary>
public class DrawingTool : MonoBehaviour
{
    // General Variables
    [Header ("General Variables")]
    public Camera mainCamera;
    public ToolType toolType = ToolType.NONE;

    [Space(30)]

    // Pen Variables
    [Header("Pen Variables")]
    public List<BasicPen> availablePens;
    [SerializeField]
    private BasicPen chosenPen;
    [SerializeField]
    private float totalDrawDistance;

    public LayerMask cantDrawOverLayer;
    private int cantDrawOverLayerIndex;

    private BasicPen drawnObject;

    [Space(30)]

    // Brush Variables
    [Header("Brush Variables")]
    public List<BasicBrush> availableBrushs;
    [SerializeField]
    private BasicBrush chosenBrush;

    private Vector2 beginPosition;
    private Vector2 endPosition;
    /// <summary>
    /// General Functions
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");

    }

    private void Update()
    {
        if (toolType == ToolType.PEN)
        {
            if (Input.GetMouseButtonDown(0) && GameManager.remainInk > 0)
            {
                beginPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                BeginDraw();
            }
            if (null != drawnObject && GameManager.remainInk > 0)
            {
                Draw();
            }
            if (Input.GetMouseButtonUp(0) || GameManager.remainInk < 0)
            {

                EndDraw();
            }
        }
        else if (toolType == ToolType.BRUSH)
        {
            // if mouse is on a mutable object, change it's properties
            if(Input.GetMouseButtonDown(0) && GameManager.remainInk > 0)
            {
                Brush();
            }
        }
    }

    public void SwitchTools()
    {
        
    }

    /// <summary>
    /// Pen Drawing Functions
    /// </summary>
    // Start drawing
    void BeginDraw()
    {
        if (GameManager.remainInk > 0)
        {
            chosenPen.InitializePen();
            // Instantiate line prefab
            drawnObject = Instantiate(chosenPen, this.transform).GetComponent<BasicPen>();
            // Set parameters
            drawnObject.UsePhysics(false);

            drawnObject.AddPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    // Drawing the line
    void Draw()
    {
        var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Prevent crossing between lines
        RaycastHit2D hit = Physics2D.CircleCast(pos, chosenPen.lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);
        if (hit)
            EndDraw();
        else
        {
            // add remain ink
            float distance = Vector2.Distance(pos, drawnObject.points[^1]);
            totalDrawDistance += distance;
            if (distance > drawnObject.linePointsMinStep)
            {
                GameManager.Instance.updateInk(distance);
                drawnObject.AddPoint(pos);
            }            
        }
    }

    // The end of drawing
    void EndDraw()
    {
        endPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (null == drawnObject) return;
        if (drawnObject.pointCount < 2)
        {
            Destroy(drawnObject.gameObject);
        }else if (drawnObject.isStraight)
        {
            Redraw();
        }
        else
        {
            drawnObject.GetComponent<Rigidbody2D>().mass = drawnObject.massRatio * drawnObject.massRatioOffset * totalDrawDistance;
            drawnObject.gameObject.layer = cantDrawOverLayerIndex;
            drawnObject.UsePhysics(true);
            drawnObject = null;
        }
    }

    /// <summary>
    /// Brush Fucntions
    /// </summary>
    void Brush()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Color.yellow, 1000f);
        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Mutable Object"))
            {
                chosenBrush.changeProperties(hit.transform.gameObject);
                GameManager.Instance.updateInk(5);
            }
        }
    }

    void Redraw()
    {
        Destroy(drawnObject.gameObject);
        chosenPen.InitializePen();
        drawnObject = Instantiate(chosenPen, this.transform).GetComponent<BasicPen>();
        drawnObject.UsePhysics(false);
        drawnObject.AddPoint(beginPosition);
        drawnObject.AddPoint(endPosition);
        totalDrawDistance = Vector2.Distance(beginPosition, endPosition);
        drawnObject.GetComponent<Rigidbody2D>().mass = drawnObject.massRatio * drawnObject.massRatioOffset * totalDrawDistance;
        drawnObject.gameObject.layer = cantDrawOverLayerIndex;
        drawnObject.UsePhysics(true);
        drawnObject = null;
    }

    /// <summary>
    /// Pick up a tool
    /// </summary>
    public void PickUpTool(ToolType toolType, GameObject toolPrefab)
    {
        if(toolType == ToolType.PEN)
        {
            BasicPen pickupPen = toolPrefab.GetComponent<BasicPen>();
            if (!availablePens.Contains(pickupPen))
            {
                availablePens.Add(pickupPen);
            }
        }else if(toolType == ToolType.BRUSH)
        {
            BasicBrush pickupBrush = toolPrefab.GetComponent<BasicBrush>();
            if (!availableBrushs.Contains(pickupBrush))
            {
                availableBrushs.Add(pickupBrush);
            }
        }
    }
}
