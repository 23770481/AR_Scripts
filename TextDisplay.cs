/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
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
*/

/*
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using Vuforia;

public class TextDisplay : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private WebScraper webScraper; //new

    private void Start()
    {
        // Find the WebScraper script on the same or another GameObject.
        //WebScraper webScraper = FindObjectOfType<WebScraper>(); //old
        webScraper = FindObjectOfType<WebScraper>(); //new

        // Check if the WebScraper script was found and has scraped data.
        if (webScraper != null && !string.IsNullOrEmpty(webScraper.scrapedData))
        {
            // Set the TextMeshPro text to the scraped data.
            textMeshPro.text = webScraper.scrapedData;
        }
        else
        {
            // Handle the case when the scraped data is not available.
            textMeshPro.text = "Scraped data not available.";
        }
    }

    // Other variables and methods as needed.
}


*/





/*
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using Vuforia;
//using WebScraper;

public class TextDisplay : MonoBehaviour
{
    //public TextMeshPro textMeshPro;
    //public TMP_Text textMeshPro;
    public TextMeshProUGUI textMeshPro;
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
            textMeshPro.text = webScraper.scrapedData;
            //TMPdata = textMeshPro.text;
            
        }
        else
        {
            // Handle the case when the scraped data is not available.
            Debug.Log("TMP Data not available");
            textMeshPro.text = "Scraped data not available.";
        }
    }

    // Other variables and methods as needed.
}


*/


// OLD WORKING SCRIPT
/*
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using Vuforia;
//using WebScraper;

public class TextDisplay : MonoBehaviour
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





/*

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using Vuforia;
//using WebScraper;

public class TextDisplay : MonoBehaviour
{
    //public TextMeshPro textMeshPro;
    //public TMP_Text textMeshPro;
    public TMP_Text PriceText;
    private string pricePrinterStr;
    //public TMP_Text PriceTextPrinter;
    private WebScraper webScraper;
    //private WebScraper price;
    private float price;
    //private string TMPdata;

    private float powerPrinter = 300.0f;
    private float useTime = 1.0f;
    private float kWhPrinter; //= ((powerPrinter * useTime) / 1000);
    private float pricePrinter; //= kWhPrinter * price;
    private string stringPrice;

    private void Start()
    {
        // Find the WebScraper script on the same or another GameObject.
        webScraper = FindObjectOfType<WebScraper>();

        // Subscribe to the scraping completion event.
        if (webScraper != null)
        {
            webScraper.OnScrapingComplete += OnScrapingComplete;
        }
    }*/
/*
private void OnScrapingComplete()
{
    // Check if the WebScraper script has scraped data.
    if (!string.IsNullOrEmpty(webScraper.scrapedData))
    {
        // Set the TextMeshPro text to the scraped data.
        Debug.Log("TMP Data: " + webScraper.scrapedData);
        //PriceText.text = webScraper.scrapedData;
        //stringPrice = webScraper.scrapedData;
        //price = float.Parse(stringPrice);
        price = float.Parse(webScraper.scrapedData);
        //TMPdata = textMeshPro.text;

        //float powerPrinter = 300.0f;
        //float useTime = 1.0f;
        kWhPrinter = ((powerPrinter * useTime) / 1000);
        pricePrinter = kWhPrinter * price;
        //pricePrinterStr = pricePrinter.ToString();
        //Debug.Log(pricePrinterStr);
        PriceText.text = pricePrinter.ToString("F2");

    }
    else
    {
        // Handle the case when the scraped data is not available.
        Debug.Log("TMP Data not available");
        PriceText.text = "Scraped data not available.";
        price = 2.8f;
    }



}*/

/*
private void OnScrapingComplete()
{
    // Check if the WebScraper script has scraped data.
    if (!string.IsNullOrEmpty(webScraper.scrapedData))
    {
        // Set the TextMeshPro text to the scraped data.
        Debug.Log("TMP Data: " + webScraper.scrapedData);
        stringPrice = webScraper.scrapedData;
        stringPrice = stringPrice.Trim();

        // Attempt to parse the string into a float
        if (float.TryParse(stringPrice, out price))
        //if (float.TryParse(stringPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out price))
        //if (float.TryParse(webScraper.scrapedData, out price))
        {
            kWhPrinter = ((powerPrinter * useTime) / 1000);
            pricePrinter = kWhPrinter * price;
            pricePrinterStr = pricePrinter.ToString();
            PriceText.text = pricePrinterStr;
        }
        else
        {
            // Handle the case where parsing fails
            //Debug.LogError("Failed to parse price from string: " + stringPrice);
            PriceText.text = "Invalid price format";
        }
    }
    else
    {
        // Handle the case when the scraped data is not available.
        Debug.Log("TMP Data not available");
        PriceText.text = "Scraped data not available.";
        price = 2.8f;
    }
}


// Other variables and methods as needed.
}*/

/*

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public TMP_Text PriceText;
    //public TextMeshPro PriceText;
    private WebScraper webScraper;
    
    private double powerPrinter = 300.0;
    private double useTime = 1.0;
    private double kWhPrinter;
    private double price;
    //public string[] Device = new string[] {"Printer", "Fan" };
    public string Device;

    private void Start()
    {
        // Find the WebScraper script on the same or another GameObject.
        webScraper = FindObjectOfType<WebScraper>();
        //PriceText = GetComponent<TMP_Text > ();

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
            // Get the scraped data as a string.
            string scrapedData = webScraper.scrapedData;
            Debug.Log("Scraped data in TextDisplay sript: " + scrapedData);
            PriceText.text = scrapedData;

            //double awe = double.Parse(scrapedData);
            //Debug.Log("awe double: " + awe);
            string awe = "2.7";
            double aweDouble = double.Parse(awe, System.Globalization.CultureInfo.InvariantCulture);
            //double aweDouble = double.Parse(awe);
            Debug.Log("awe double: " + aweDouble);
            aweDouble = aweDouble + 0.4;
            Debug.Log("awe double: " + aweDouble);

            //string txt = a.ToString(System.Globalization.CultureInfo.InvariantCulture);

            string aweStr = aweDouble.ToString(System.Globalization.CultureInfo.InvariantCulture);
            Debug.Log("awe string: " + aweStr);

            //string hey = 

            PriceText.text = aweStr;


            //PriceText.text = aweStr;
            // Attempt to parse the string into a float.
            if (double.TryParse(scrapedData, out price))
            {
                // Perform mathematical operations on 'price'.
                kWhPrinter = (powerPrinter * useTime) / 1000;
                double result = kWhPrinter * price;

                // Convert the result back to a string and display it.
                string resultStr = result.ToString();
                //PriceText.text = resultStr;
            }
            else
            {
                // Handle the case where parsing fails.
                //PriceText.text = "Invalid price format";
            }
        }
        else
        {
            // Handle the case when the scraped data is not available.
            //PriceText.text = "Scraped data not available.";
            //price = 2.8f; // You can set a default value if needed.
        }
    }
}
*/


using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
//using System.Linq;
public class TextDisplay : MonoBehaviour
{
    public TMP_Text PriceText;
    //public TMP_Text PriceText;
    //public TMP_Text kWhText;
    private WebScraper webScraper;
    public string device;

    private double time = 1.0;
    private double power;
    private double kWh;
    private double price;
    private double cost;
    private double uniqueCost;
    //public double uniqueCost;
    //public string[] Device = new string[] {"Printer", "Fan" };

    //coffee fan grill heater printer

    private void Start()
    {
        
        if (device == "printer")
        {
            power = 300;
            //Debug.Log("PRINTER");
        }
        else if (device == "fan")
        {
            power = 50;
        }
        else if (device == "grill")
        {
            power = 700;
        }
        else if (device == "heater")
        {
            power = 2000;
        }
        else if (device == "coffee")
        {
            power = 1200;
        }
        else
        {
            power = 500;
        }


        // Find the WebScraper script on the same or another GameObject.
        webScraper = FindObjectOfType<WebScraper>();

        // Subscribe to the scraping completion event.
        if (webScraper != null)
        {
            webScraper.OnScrapingComplete += OnScrapingComplete;
        }

        //Debug.Log("Unique Cost: " + uniqueCost);
        //GlobalTotalManager.Instance.UpdateTotal(uniqueCost);
    }

    //private void OnTrackingFound()
    //{
    //    Debug.Log("Unique Cost: " + uniqueCost);
    //    GlobalTotalManager.Instance.UpdateTotal(uniqueCost);
    //}

    private void OnScrapingComplete()
    {
        // Check if the WebScraper script has scraped data.
        if (!string.IsNullOrEmpty(webScraper.scrapedData))
        {
            // Get the scraped data as a string.
            //string scrapedData = webScraper.scrapedData;
            //Debug.Log("Scraped data in TextDisplay sript: " + scrapedData);
            //PriceText.text = scrapedData;

            string scrapedData = webScraper.scrapedData;
            price = double.Parse(scrapedData, System.Globalization.CultureInfo.InvariantCulture);
            //Debug.Log("Price From WebScraper: " + price);
            //PriceText.text = scrapedData;

            /*
            kWh = (power * time) / 1000;
            cost = kWh * price;

            string priceStr = price.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string kWhStr = kWh.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string costStr = cost.ToString(System.Globalization.CultureInfo.InvariantCulture);
            */

        }
        else
        {
            // Handle the case when the scraped data is not available.
            //PriceText.text = "Scraped data not available.";
            price = 2.8; // You can set a default value if needed.
        }

        kWh = (power * time) / 1000;
        cost = kWh * price;

        uniqueCost = cost;

        if (device == "printer")
        {
            GlobalTotalManager.Instance.GetPrinterPrice(uniqueCost);
        }
        else if (device == "fan")
        {
            GlobalTotalManager.Instance.GetFanPrice(uniqueCost);
        }
        else if (device == "grill")
        {
            GlobalTotalManager.Instance.GetGrillPrice(uniqueCost);
        }
        else if (device == "heater")
        {
            GlobalTotalManager.Instance.GetHeaterPrice(uniqueCost);
        }
        else if (device == "coffee")
        {
            GlobalTotalManager.Instance.GetCoffeePrice(uniqueCost);
        }

        //GlobalTotalManager.Instance.runningTotal += uniqueCost;
        //GlobalTotalManager.Instance.UpdateTotal(uniqueCost);
        //Debug.Log("Unique Cost: " + uniqueCost);

        string priceStr = price.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string powerStr = power.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string kWhStr = kWh.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string costStr = cost.ToString(System.Globalization.CultureInfo.InvariantCulture);



        string info = "Electricity Price (per kWh):  R" + priceStr + "\n\n"
            + "Power Rating:  " + powerStr + "W" + "\n\n"
            + "Device consumption per hour:  " + kWhStr + "kWh" + "\n\n"
            + "Cost of using device for an hour:  R" + costStr; // + "\n";
        PriceText.text = info;
    }


}

