using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTeleport : MonoBehaviour
{
    private Vector3 destination;
    private bool isActivated;
    public GameObject teleportDestination;

    // for enable and disable drawing tool
    [Header("Drawing tool settings")]
    private Transform grid;
    public bool useButtonSetting;

    public bool setEraserPenButton;
    public bool setMagnetBrushButton;
    public bool setGravityBrushButton;
    public bool setPlatformPenButton;
    public bool setRockPenButton;
    public bool setElectronicPenButton;

    

    void Start()
    {
        destination = teleportDestination.transform.position;
        isActivated = true;
        grid = GameManager.Instance.levelUI.transform.Find("PenAndBrushButtons").transform.Find("Grid");
        setEraserPenButton = true;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ElectronicDevice") && isActivated)
        {
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 2.0)
            {
                collision.gameObject.transform.position = destination;
            }
        }
        if (collision.gameObject.CompareTag("Mutable Object") && isActivated)
        {
            float rectWidth = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            float rectHeight = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            Vector3 rectRightCenter = new Vector3(collision.gameObject.transform.position.x + rectWidth / 2, collision.gameObject.transform.position.y, 0);
            Vector3 rectLeftCenter = new Vector3(collision.gameObject.transform.position.x - rectWidth / 2, collision.gameObject.transform.position.y, 0);
            Vector3 rectUpCenter = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + rectHeight / 2, 0);
            Vector3 rectDownCenter = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - rectHeight / 2, 0);
            if (System.Math.Abs(Vector3.Distance(rectRightCenter, this.gameObject.transform.position)) < 2.0 || System.Math.Abs(Vector3.Distance(rectLeftCenter, this.gameObject.transform.position)) < 2.0 ||
                System.Math.Abs(Vector3.Distance(rectUpCenter, this.gameObject.transform.position)) < 2.0 || System.Math.Abs(Vector3.Distance(rectDownCenter, this.gameObject.transform.position)) < 2.0)
            {
                collision.gameObject.transform.position = destination;

            }
            //float rectRightCenterDistance = rectRightCenter.x - collision.gameObject.transform.position.x;
            //float rectLeftCenterDistance = collision.gameObject.transform.position.x - rectLeftCenter.x;
            //if (System.Math.Abs(collision.gameObject.transform.position.x + rectRightCenterDistance - this.gameObject.transform.position.x) < 2.0 || System.Math.Abs(collision.gameObject.transform.position.x - rectLeftCenterDistance - this.gameObject.transform.position.x) < 2.0)
            //{
            //    collision.gameObject.transform.position = destination;
            //    otherTeleport.connected = false;
            //    yield return new WaitForSeconds(0.2f);
            //    otherTeleport.connected = true;
            //}
        }
        if (collision.gameObject.CompareTag("Player") && isActivated)
        {
            GameManager.Instance.player.transform.position = destination;
            if (useButtonSetting)
                setButtons();
            isActivated = false;
            //When we need two way transport:
            //teleportDestination.GetComponent<OneWayTeleport>().isActivated = false;
            yield return new WaitForSeconds(2.0f);
            isActivated = true;
            //teleportDestination.GetComponent<OneWayTeleport>().isActivated = true;
        }
    }


    private void setButtons()
    {
        grid.Find("EraserPenButton").gameObject.SetActive(setEraserPenButton);
        grid.Find("MagnetBrushButton").gameObject.SetActive(setMagnetBrushButton);
        grid.Find("GravityBrushButton").gameObject.SetActive(setGravityBrushButton);
        grid.Find("PlatformPenButton").gameObject.SetActive(setPlatformPenButton);
        grid.Find("RockPenButton").gameObject.SetActive(setRockPenButton);
        grid.Find("ElectronicPenButton").gameObject.SetActive(setElectronicPenButton);
    }
}
