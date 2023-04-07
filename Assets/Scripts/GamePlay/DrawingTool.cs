using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ToolType { PEN = 0, BRUSH, NONE };
public enum BrushType { NONE = 0, GRAVITY = 1, MAGNET_POS = 2, MAGNET_NEG = 3, ERASER = 4};

/// <summary>
/// Drawing Tool controls every pen and brush in the scene
/// </summary>
public class DrawingTool : MonoBehaviour
{
    
    private PlayerController playerControl;
    // General Variables
    [Header ("General Variables")]
    public Texture2D cantDrawSign;
    public Camera mainCamera;
    public ToolType toolType = ToolType.NONE;

    [Space(30)]

    // Pen Variables
    [Header("Pen Variables")]
    public List<BasicPen> availablePens = new();
    [SerializeField]
    private BasicPen chosenPen;
    [SerializeField]
    private float totalDrawDistance;

    public LayerMask cantDrawOverLayer;
    private int cantDrawOverLayerIndex;
    private int electronicPenIndex;
    private bool mouseSecondaryButton = false;
    public List<GameObject> electronicPenInstance;

    private BasicPen drawnObject;

    [Space(30)]

    // Brush Variables
    [Header("Brush Variables")]
    public List<BasicBrush> availableBrushes = new();
    [SerializeField]
    private BasicBrush chosenBrush;

    private Vector2 cursorHotsopt;


    private Vector2 beginPosition;
    private Vector2 endPosition;
    /// <summary>
    /// General Functions
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        electronicPenIndex = LayerMask.NameToLayer("ElectronicPen");
        Cursor.SetCursor(cantDrawSign, Vector2.zero, CursorMode.Auto);
        playerControl = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (toolType == ToolType.PEN)
        {
            if(chosenPen.name == "EraserPen")
            {
                if (Input.GetMouseButton(0))
                {
                    Erase();
                }
            }
            else
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
                    Cursor.SetCursor(chosenPen.cursor, new Vector2(20, 20), CursorMode.Auto);
                }
            }
        }
        else if (toolType == ToolType.BRUSH)
        {
            // if mouse is on a mutable object, change it's properties
            if(Input.GetMouseButtonDown(0) && GameManager.remainInk > 0)
            {
                Brush();
            } else if (Input.GetMouseButtonDown(1) && GameManager.RemainInk > 0){
                mouseSecondaryButton = true;
                Brush();
                mouseSecondaryButton = false;
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                Cursor.SetCursor(chosenBrush.cursor, new Vector2(chosenBrush.cursor.width / 2, chosenBrush.cursor.height / 2), CursorMode.Auto);
            }
        }
    }

    private Image FindBanner(GameObject button)
    {
        Image[] images = button.GetComponentsInChildren<Image>();

        foreach (Image image in images)
        {
            if (image.name == "SelectedBanner")
            {
                return image;
            }
        }
        return null;
    }

    public void SwitchTools(GameObject toolPrefab)
    {
        if (toolPrefab.name.EndsWith("Pen"))
        {
            BasicPen pen = toolPrefab.GetComponent<BasicPen>();
            if (chosenPen == pen)
            {
                HideElectronicPenInstance();
                //Cancel selection
                this.toolType = ToolType.NONE;
                chosenPen = null;
                Cursor.SetCursor(cantDrawSign, Vector2.zero, CursorMode.Auto);
                GameObject chosenButton = GameObject.Find(pen.penName + "Button");
                Image img = FindBanner(chosenButton);
                img.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-transparent");
                //TextMeshProUGUI tmp = chosenButton.GetComponentInChildren<TextMeshProUGUI>();
                //tmp.SetText(tmp.text.Replace("-", ""));
            }
            else
            {
                //Cancel current selection
                if(chosenPen != null)
                {
                    HideElectronicPenInstance();
                    if (pen.name == "ElectronicPen" || pen.name == "EraserPen")
                    {
                        ShowElectronicPenInstance();
                    }
                    GameObject chosenPenButton = GameObject.Find(chosenPen.penName + "Button");
                    Image buttonImg = FindBanner(chosenPenButton);
                    buttonImg.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-transparent");
                    //TextMeshProUGUI buttonTmp = chosenPenButton.GetComponentInChildren<TextMeshProUGUI>();
                    //buttonTmp.SetText(buttonTmp.text.Replace("-", ""));
                    chosenPen = null;
                }
                if(chosenBrush != null)
                {
                    ShowElectronicPenInstance();
                    GameObject chosenBrushButton = GameObject.Find(chosenBrush.brushName + "Button");
                    Image buttonImg = FindBanner(chosenBrushButton);
                    buttonImg.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-transparent");
                    //TextMeshProUGUI buttonTmp = chosenBrushButton.GetComponentInChildren<TextMeshProUGUI>();
                    //buttonTmp.SetText(buttonTmp.text.Replace("-", ""));
                    chosenBrush = null;
                }
                chosenBrush = null;
                //Change to new selected tool
                this.toolType = ToolType.PEN;
                if (pen.name == "ElectronicPen" || pen.name == "EraserPen")
                {
                    ShowElectronicPenInstance();
                }
                chosenPen = pen;
                //cursorHotsopt = new Vector2(pen.cursor.width / 2, pen.cursor.height / 2);
                cursorHotsopt = new Vector2(20, 20);
                Cursor.SetCursor(pen.cursor, cursorHotsopt, CursorMode.Auto);
                GameObject chosenButton = GameObject.Find(pen.penName + "Button");
                Image img = FindBanner(chosenButton);
                img.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-" + pen.penName);
                //TextMeshProUGUI tmp = chosenButton.GetComponentInChildren<TextMeshProUGUI>();
                //tmp.SetText("-" + tmp.text.Substring(0, tmp.text.IndexOf("\n")) + "-" + tmp.text.Substring(tmp.text.IndexOf("\n")));
            }
        }else if(toolPrefab.name.EndsWith("Brush"))
        {
            HideElectronicPenInstance();
            BasicBrush brush = toolPrefab.GetComponent<BasicBrush>();
            if (chosenBrush == brush)
            {
                //Cancel Selection
                this.toolType = ToolType.NONE;
                chosenBrush = null;
                Cursor.SetCursor(cantDrawSign, Vector2.zero, CursorMode.Auto);
                GameObject chosenButton = GameObject.Find(brush.brushName + "Button");
                Image img = FindBanner(chosenButton);
                img.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-transparent");
                //TextMeshProUGUI tmp = chosenButton.GetComponentInChildren<TextMeshProUGUI>();
                //tmp.SetText(tmp.text.Replace("-", ""));
            }
            else
            {
                //Cancel current selection
                if (chosenPen != null)
                {
                    GameObject chosenPenButton = GameObject.Find(chosenPen.penName + "Button");
                    Image buttonImg = FindBanner(chosenPenButton);
                    buttonImg.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-transparent");
                    //TextMeshProUGUI buttonTmp = chosenPenButton.GetComponentInChildren<TextMeshProUGUI>();
                    //buttonTmp.SetText(buttonTmp.text.Replace("-", ""));
                    chosenPen = null;
                }
                if (chosenBrush != null)
                {
                    GameObject chosenBrushButton = GameObject.Find(chosenBrush.brushName + "Button");
                    Image buttonImg = FindBanner(chosenBrushButton);
                    buttonImg.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-transparent");
                    //TextMeshProUGUI buttonTmp = chosenBrushButton.GetComponentInChildren<TextMeshProUGUI>();
                    //buttonTmp.SetText(buttonTmp.text.Replace("-", ""));
                    chosenBrush = null;
                }
                //Change to new selected tool
                this.toolType = ToolType.BRUSH;
                if (brush.name == "EraserBrush")
                {
                    ShowElectronicPenInstance();
                }
                chosenBrush = brush;
                cursorHotsopt = new Vector2(brush.cursor.width / 2, brush.cursor.height / 2);
                Cursor.SetCursor(brush.cursor, cursorHotsopt, CursorMode.Auto);
                GameObject chosenButton = GameObject.Find(brush.brushName + "Button");
                Image img = FindBanner(chosenButton);
                img.sprite = Resources.Load<Sprite>("Sprites/art/UI-LevelPage/banner-" + brush.brushName);
                //TextMeshProUGUI tmp = chosenButton.GetComponentInChildren<TextMeshProUGUI>();
                //tmp.SetText("-" + tmp.text.Substring(0, tmp.text.IndexOf("\n")) + "-" + tmp.text.Substring(tmp.text.IndexOf("\n")));
            }
        }
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
        if (hit && chosenPen.name != "ElectronicPen")
        {
            //cursorHotsopt = new Vector2(pen.cursor.width / 2, pen.cursor.height / 2);
            if (hit.transform.name != "UIBlocker")
            {
                Cursor.SetCursor(cantDrawSign, new Vector2(cantDrawSign.width / 2, cantDrawSign.height / 2), CursorMode.Auto);
            }
            EndDraw();
        }
        //else if (hit.transform.name == "UIBlocker" && chosenPen.name == "ElectronicPen")
        //{
        //    EndDraw();
        //}
        else
        {
            // add remain ink
           
            float distance = Vector2.Distance(pos, drawnObject.points[^1]);
            totalDrawDistance += distance;
            if (distance > drawnObject.linePointsMinStep)
            {
                GameManager.Instance.updateInk(distance * chosenPen.lineCostFactor);
                drawnObject.AddPoint(pos);
            }
            switch(chosenPen.penName){
                case "PlatformPen":
                    GameManager.platformInkUsed += distance * chosenPen.lineCostFactor;
                    break;
                case "RockPen":
                    GameManager.rockInkUsed += distance * chosenPen.lineCostFactor;
                    break;
                case "WoodPen":
                    GameManager.woodInkUsed += distance * chosenPen.lineCostFactor;
                    break;
                default:
                    break;
            }            
        }
    }

    void Erase()
    {
        var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Prevent crossing between lines
        RaycastHit2D hit = Physics2D.CircleCast(pos, chosenPen.lineWidth, Vector2.zero);
        if(hit && hit.transform.CompareTag("Drawn Object"))
        {
            Destroy(hit.transform.gameObject);
        }
    }

    // The end of drawing
    void EndDraw()
    {
        //Debug.Log("enddraw");
        endPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (null == drawnObject) return;
        if (drawnObject.pointCount < 3)
        {
            Destroy(drawnObject.gameObject);
        }else if (drawnObject.isStraight)
        {
            Redraw();
        }else if (chosenPen.name == "ElectronicPen")
        {
            electronicPenInstance.Add(drawnObject.GetComponent<ElectronicPen>().GetObject());
            drawnObject.GetComponent<Rigidbody2D>().mass = drawnObject.massRatio * drawnObject.massRatioOffset * totalDrawDistance;
            totalDrawDistance = 0;
            drawnObject.gameObject.layer = electronicPenIndex;
            drawnObject.UsePhysics(true);
            drawnObject = null;
        }
        else
        {
            drawnObject.GetComponent<Rigidbody2D>().mass = drawnObject.massRatio * drawnObject.massRatioOffset * totalDrawDistance;
            totalDrawDistance = 0;
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
        BrushType currentBrush = BrushType.NONE;
        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Mutable Object"))
            {
                if (chosenBrush.name != "EraserBrush")
                {   
                    switch(chosenBrush.brushName){
                        case "GravityBrush":
                            GameManager.gravityInkUsed += chosenBrush.brushCost;
                            currentBrush = BrushType.GRAVITY;
                            break;
                        case "MagnetBrush":
                            Texture2D positiveCursor = Resources.Load<Texture2D>("Sprites/Cursors/MagnetBrush-S");
                            Texture2D negativeCursor = Resources.Load<Texture2D>("Sprites/Cursors/MagnetBrush-N");
                            GameManager.magnetInkUsed += chosenBrush.brushCost;
                            currentBrush = mouseSecondaryButton == true ? BrushType.MAGNET_POS : BrushType.MAGNET_NEG;
                            chosenBrush.cursor = mouseSecondaryButton == true ? positiveCursor : negativeCursor;
                            Cursor.SetCursor(chosenBrush.cursor, new Vector2(chosenBrush.cursor.width / 2, chosenBrush.cursor.height / 2), CursorMode.Auto);
                            break;
                        default:
                            break;
                    }
                    
                    chosenBrush.changeProperties(hit.transform.gameObject, currentBrush);
                    GameManager.Instance.updateInk(chosenBrush.brushCost);
                }
            }
            if (hit.transform.CompareTag("Shrine")){
                if(chosenBrush.name != "EraserBrush"){
                    playerControl.OnShrineBrushed(chosenBrush);
                }
            }
            if (chosenBrush.name == "EraserBrush" && hit.transform.CompareTag("Drawn Object"))
            {
                currentBrush = BrushType.ERASER;
                chosenBrush.changeProperties(hit.transform.gameObject, currentBrush);
                GameManager.Instance.updateInk(chosenBrush.brushCost);
                // update eraser brush cost
                GameManager.eraserInkUsed += chosenBrush.brushCost;
            }
            else if (hit.transform.CompareTag("Platform Object"))
            {
                //Debug.Log("platform");
                Cursor.SetCursor(cantDrawSign, new Vector2(cantDrawSign.width / 2, cantDrawSign.height / 2), CursorMode.Auto);
            }
        }
    }

    void Redraw()
    {
        Destroy(drawnObject.gameObject);
        chosenPen.InitializePen();
        drawnObject = Instantiate(chosenPen, this.transform).GetComponent<BasicPen>();
        drawnObject.UsePhysics(false);
        totalDrawDistance = Vector2.Distance(beginPosition, endPosition);
        drawnObject.AddPoint(beginPosition);
        int numOfPoints = (int)(totalDrawDistance / 0.1f);
        for (int i = 1; i <= numOfPoints; i++)
        {
            Vector2 point = beginPosition + (endPosition - beginPosition) * (i / (numOfPoints + 1.0f));
            drawnObject.AddPoint(point);
        }

        drawnObject.AddPoint(endPosition);
        
        drawnObject.GetComponent<Rigidbody2D>().mass = drawnObject.massRatio * drawnObject.massRatioOffset * totalDrawDistance;
        totalDrawDistance = 0;
        drawnObject.gameObject.layer = cantDrawOverLayerIndex;
        drawnObject.UsePhysics(true);
        drawnObject = null;
    }

    void HideElectronicPenInstance()
    {
        foreach (GameObject elecPen in electronicPenInstance)
        {
            if(elecPen != null)
            {
                elecPen.SetActive(false);
            }
        }
    }
    void ShowElectronicPenInstance()
    {
        foreach (GameObject elecPen in electronicPenInstance)
        {
            if (elecPen != null)
            {
                elecPen.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Pick up a tool
    /// </summary>
    public void PickUpTool(ToolType toolType, GameObject toolPrefab, GameObject toolButton)
    {
        if(toolType == ToolType.PEN)
        {
            BasicPen pickupPen = toolPrefab.GetComponent<BasicPen>();
            availablePens.Add(pickupPen);
        }
        else if(toolType == ToolType.BRUSH)
        {
            BasicBrush pickupBrush = toolPrefab.GetComponent<BasicBrush>();
            availableBrushes.Add(pickupBrush);
        }
        toolButton.SetActive(true);
    }
}
