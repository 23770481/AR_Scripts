
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


/*
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using Vuforia;
//using WebScraper;

public class GetData : MonoBehaviour
{
    //public TextMeshPro textMeshPro;
    //public TMP_Text textMeshPro;
    public TMP_Text PriceText;
    private WebScraper webScraper;
    //private string TMPdata;

    private void Start()
    {
        // Find the WebScraper script on the same or another GameObject.
        webScraper = FindObjectOfType<WebScraper>();

        // Subscribe to the scraping completion event.
        if (webScraper != null)
        {
            webScraper.OnScrapingComplete += OnScrapingComplete;
        }
    }

    private void OnScrapingComplete()
    {
        // Check if the WebScraper script has scraped data.
        if (!string.IsNullOrEmpty(webScraper.scrapedData))
        {
            // Set the TextMeshPro text to the scraped data.
            Debug.Log("TMP Data: " + webScraper.scrapedData);
            PriceText.text = webScraper.scrapedData;
            //TMPdata = textMeshPro.text;

        }
        else
        {
            // Handle the case when the scraped data is not available.
            Debug.Log("TMP Data not available");
            PriceText.text = "Scraped data not available.";
        }
    }

    // Other variables and methods as needed.
}
*/