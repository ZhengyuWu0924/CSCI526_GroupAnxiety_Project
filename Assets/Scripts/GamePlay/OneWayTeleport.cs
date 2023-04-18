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
        if ((collision.gameObject.CompareTag("Mutable Object") || collision.gameObject.CompareTag("ElectronicDevice")) && isActivated)
        {
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 2.0)
            {
                collision.gameObject.transform.position = destination;
            }
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
