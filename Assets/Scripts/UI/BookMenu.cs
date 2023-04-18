using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookMenu : MonoBehaviour
{
    public Sprite[] notes;
    public GameObject book;
    public GameObject pageObject;
    public GameObject instructionScreen;
    private Image page;
    private int currentPageNum;

    public int unlockedPages;

    private int totalPageNum = 10;

    public GameObject prevButton;
    public GameObject nextButton;

    void Start()
    {
        page = pageObject.GetComponent<Image>();
    }

    void RefreshButton()
    {
        prevButton.SetActive(true);
        nextButton.SetActive(true);

        if (currentPageNum == 0)
        {
            prevButton.SetActive(false);

        }

        if (currentPageNum == unlockedPages - 1)
        {
            nextButton.SetActive(false);
        }
        
    }


    public void OpenPage(int pageNum)
    {
        instructionScreen.SetActive(false);
        book.SetActive(true);
        if (pageNum >= 0 && pageNum < unlockedPages)
        {
            page.sprite = notes[pageNum];
            currentPageNum = pageNum;
        }
        RefreshButton();
    }

    public void OpenNotes()
    {
        book.SetActive(true);
        currentPageNum = 0;
        OpenPage(currentPageNum);
    }

    public void CloseNotes()
    {
        book.SetActive(false);
    }

    public void OpenNextPage()
    {
        currentPageNum++;
        page.sprite = notes[currentPageNum];
        RefreshButton();
    }

    public void OpenPrevPage()
    {
        currentPageNum--;
        page.sprite = notes[currentPageNum];
        RefreshButton();
    }

}
