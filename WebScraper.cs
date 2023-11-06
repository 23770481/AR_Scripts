
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebScraper : MonoBehaviour
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



/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;

public class WebScraper : MonoBehaviour
{
    private async void Start()
    {
        string url = "https://example.com"; // Replace with the URL you want to scrape.

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();
                // Parse and process the HTML content here.
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}

*/


/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class WebScrapingScript : MonoBehaviour
{
    private string scrapedData; // Variable to store the scraped data

    public async Task PerformScrapingAsync(string url)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();
                // Parse and process the HTML content here.

                // Store the scraped data in the 'scrapedData' variable.
                scrapedData = htmlContent;

                // Print the scraped data to the debug console.
                Debug.Log("Scraped Data:\n" + scrapedData);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }

    public string GetScrapedData()
    {
        return scrapedData;
    }
}
*/
//using HtmlAgilityPack;


//////////////////////////////////////////////////////////////////////////////////////////
///
/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;


public class WebScraper : MonoBehaviour
{
    private string scrapedData; // Variable to store the scraped data

    // URL to scrape (you can change this in the Unity Inspector). 
    [SerializeField]
    private string scrapeUrl = "https://example.com";
    

    private async void Start()
    {
        // Start the web scraping process.
        await PerformScrapingAsync();
    }

    private async Task PerformScrapingAsync()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(scrapeUrl);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();
                // Parse and process the HTML content here.

                // Store the scraped data in the 'scrapedData' variable.
                scrapedData = htmlContent;

                // Print the scraped data to the debug console.
                Debug.Log("Scraped Data:\n" + scrapedData);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }

    public string GetScrapedData()
    {
        return scrapedData;
    }
}
/*
//////////////////////////////////////////////////////////////////////////////////////////

/*
using System;
using System.Net.Http;
using UnityEngine;

public class SimpleWebScraping : MonoBehaviour
{
    private async void Start()
    {
        string url = "https://example.com"; // Replace with the URL you want to scrape.

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();

                // Find a specific pattern or keyword in the HTML content and extract the data.
                string keyword = "Your Data Keyword Here";
                int startIndex = htmlContent.IndexOf(keyword);

                if (startIndex >= 0)
                {
                    startIndex += keyword.Length;
                    int endIndex = htmlContent.IndexOf("</", startIndex);

                    if (endIndex >= startIndex)
                    {
                        string extractedData = htmlContent.Substring(startIndex, endIndex - startIndex);
                        Debug.Log("Extracted Data: " + extractedData);
                    }
                }
                else
                {
                    Debug.LogWarning("Keyword not found in HTML content.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}

*/


/*
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using HtmlAgilityPack; // Import HtmlAgilityPack namespace

public class WebScraper : MonoBehaviour
{
    //private string scrapedData; // Variable to store the scraped data
    public string scrapedData { get; private set; }

    // URL to scrape (you can change this in the Unity Inspector).
    [SerializeField]
    private string scrapeUrl = "https://example.com";

    private async void Start()
    {
        // Start the web scraping process.
        await PerformScrapingAsync();
    }

    private async Task PerformScrapingAsync()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(scrapeUrl);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();

                // Use HtmlAgilityPack to parse and extract specific data.
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                // Use XPath to select specific elements.
                ////tbody/tr[1]/td[1]   //tbody/tr[1]/td[1]
                //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='content']");


                //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//tbody/tr[1]/td[1]");

                //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("div.wrapper div:nth-child(1) table:nth-child(2) tbody:nth-child(2) tr:nth-child(1) > td:nth-child(2)");

                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[1]/td[1]");


                //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//tbody/tr[1]/td[1]");


                if (nodes != null)
                {
                    foreach (HtmlNode node in nodes)
                    {
                        // Extract and process data from the selected elements.
                        string extractedData = node.InnerText;
                        scrapedData = extractedData; // Store the scraped data
                        //public string scrapedData { get; private set; };
                        Debug.Log("Extracted Data: " + scrapedData);
                        //public string scrapedData { get; private set; };
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }

    public string GetScrapedData()
    {
        return scrapedData;
    }
}

*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using HtmlAgilityPack; 

public class WebScraper : MonoBehaviour
{

    public string scrapedData { get; private set; } 
    public event Action OnScrapingComplete;

    [SerializeField]
    private string scrapeUrl = "https://example.com";

    private async void Start()
    {

        await PerformScrapingAsync();
    }

    private async Task PerformScrapingAsync()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(scrapeUrl);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[4]/td[2]");
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[1]/td[1]");


                if (nodes != null)
                {
                    foreach (HtmlNode node in nodes)
                    {

                        string extractedData = node.InnerText;
                        scrapedData = extractedData;
                      
                        Debug.Log("Extracted Data: " + scrapedData);
                        
                    }
                }
                OnScrapingComplete?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }

    public string GetScrapedData()
    {
        return scrapedData;
    }
}