
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using HtmlAgilityPack; // Import HtmlAgilityPack namespace
using System.Text.RegularExpressions;

using UnityEngine.Events;
using Vuforia;
using TMPro;





public class LoadForecast : MonoBehaviour
{

    public string scrapedDataLoad { get; private set; }
    public event Action OnScrapingCompleteLoad;
    public TMP_Text ForecastText;
    public TMP_Text StatusText;
    private int count;


    [SerializeField]
    private string scrapeUrlLoad = "https://example.com";

    private async void Start()
    {

        await PerformScrapingAsync();
    }

    private async Task PerformScrapingAsync()
    {
        using (HttpClient httpClientLoad = new HttpClient())
        {
            try
            {
                HttpResponseMessage responseLoad = await httpClientLoad.GetAsync(scrapeUrlLoad);
                responseLoad.EnsureSuccessStatusCode();

                string htmlContentLoad = await responseLoad.Content.ReadAsStringAsync();
                Debug.Log("HTML Content: " + htmlContentLoad);


                string timestampPattern = "\"Timestamp\":\"(.*?)T(.*?)\"";
                string declaredAvailabilityPattern = "\"DeclaredAvailabilty\":(\\d+\\.\\d+)";
                string loadForecastPattern = "\"LoadForecast\":(\\d+\\.\\d+)";
                string maxAvailabilityPattern = "\"MaxAvailability\":(\\d+\\.\\d+)";
                string colorPattern = "\"Color\":\"(.*?)\"";
                string directionPattern = "\"Direction\":\"(.*?)\"";
                string directionIdPattern = "\"DirectionId\":(\\d+)";


                Regex timestampRegex = new Regex(timestampPattern);
                Regex declaredAvailabilityRegex = new Regex(declaredAvailabilityPattern);
                Regex loadForecastRegex = new Regex(loadForecastPattern);
                Regex maxAvailabilityRegex = new Regex(maxAvailabilityPattern);
                Regex colorRegex = new Regex(colorPattern);
                Regex directionRegex = new Regex(directionPattern);
                Regex directionIdRegex = new Regex(directionIdPattern);

                // Find all matches in the HTML content
                MatchCollection timestampMatches = timestampRegex.Matches(htmlContentLoad);
                MatchCollection declaredAvailabilityMatches = declaredAvailabilityRegex.Matches(htmlContentLoad);
                MatchCollection loadForecastMatches = loadForecastRegex.Matches(htmlContentLoad);
                MatchCollection maxAvailabilityMatches = maxAvailabilityRegex.Matches(htmlContentLoad);
                MatchCollection colorMatches = colorRegex.Matches(htmlContentLoad);
                MatchCollection directionMatches = directionRegex.Matches(htmlContentLoad);
                MatchCollection directionIdMatches = directionIdRegex.Matches(htmlContentLoad);


                string[] dates = new string[timestampMatches.Count];
                string[] times = new string[timestampMatches.Count];
                string[] declaredAvailabilities = new string[declaredAvailabilityMatches.Count];
                string[] loadForecasts = new string[loadForecastMatches.Count];
                string[] maxAvailabilities = new string[maxAvailabilityMatches.Count];
                string[] colors = new string[colorMatches.Count];
                string[] directions = new string[directionMatches.Count];
                string[] directionIds = new string[directionIdMatches.Count];

                int[] AvailableInt = new int[declaredAvailabilityMatches.Count];
                int[] LoadInt = new int[declaredAvailabilityMatches.Count];
                int[] BufferInt = new int[declaredAvailabilityMatches.Count];
                string[] BufferString = new string[declaredAvailabilityMatches.Count];


                // Extract and store the data
                count = timestampMatches.Count;
                for (int i = 0; i < timestampMatches.Count; i++)
                {
                    //count = i;
                    dates[i] = timestampMatches[i].Groups[1].Value;
                    times[i] = timestampMatches[i].Groups[2].Value;
                    times[i] = times[i].Substring(0, Math.Min(5, times[i].Length));
                }

                for (int i = 0; i < declaredAvailabilityMatches.Count; i++)
                {
                    declaredAvailabilities[i] = declaredAvailabilityMatches[i].Groups[1].Value;
                    declaredAvailabilities[i] = declaredAvailabilities[i].Substring(0, Math.Min(5, declaredAvailabilities[i].Length));
                    AvailableInt[i] = int.Parse(declaredAvailabilities[i], System.Globalization.CultureInfo.InvariantCulture);
                }

                for (int i = 0; i < loadForecastMatches.Count; i++)
                {
                    loadForecasts[i] = loadForecastMatches[i].Groups[1].Value;
                    loadForecasts[i] = loadForecasts[i].Substring(0, Math.Min(5, loadForecasts[i].Length));
                    LoadInt[i] = int.Parse(loadForecasts[i], System.Globalization.CultureInfo.InvariantCulture);
                }

                for (int i = 0; i < maxAvailabilityMatches.Count; i++)
                {
                    maxAvailabilities[i] = maxAvailabilityMatches[i].Groups[1].Value;
                }

                for (int i = 0; i < colorMatches.Count; i++)
                {
                    colors[i] = colorMatches[i].Groups[1].Value;
                }

                for (int i = 0; i < directionMatches.Count; i++)
                {
                    directions[i] = directionMatches[i].Groups[1].Value;
                }

                for (int i = 0; i < directionIdMatches.Count; i++)
                {
                    directionIds[i] = directionIdMatches[i].Groups[1].Value;
                }



                string colorStatus = "No data found";


                //for (int i = 0; i < count; i++)
                //{

                //}

                if (colors[5] == "Green")
                {
                    colorStatus = "The national electricity grid is currently stable. \n" + "Thank you for using electricity efficiently. \n";
                }
                else if (colors[5] == "Orange")
                {
                    colorStatus = "The national electricity grid is currently experiencing light strain. \n" + "Residential consumers are promoted to switch off: \n" +
                        "Unnecessary lights \n" +
                        "Geysers \n " +
                        "Pool pumps \n" +
                        "Air-conditioning \n" +
                        "Dishwashers \n" +
                        "Any other non-essential appliances\n ";
                }
                else if (colors[5] == "Red")
                {
                    colorStatus = "The national electricity grid is currently experiencing increasing strain. \n" +
                        "Loadshedding is imminent" +
                        "Residential consumers are promoted to switch off: \n" +
                        
                        "Unnecessary lights \n" +
                        "Geysers \n " +
                        "Pool pumps \n" +
                        "Air-conditioning \n" +
                        "Dishwashers \n" +
                        "Any other non-essential appliances\n ";
                }
                else if (colors[5] == "Black")
                {
                    colorStatus = "The national electricity grid is currently experiencing severe strain.\n\n" +
                        "Loadshedding is currently in progress\n\n" +
                        "Residential consumers are promoted to switch off all loads that are not absolutely essential\n\n" +
                        "Including:\n" +
                        "Unnecessary lights\n" +
                        "Geysers\n" +
                        "Pool pumps \n" +
                        "Air-conditioning\n" +
                        "Dishwashers"; //+
                        //"Any other non-essential appliances\n ";
                }

                Debug.Log(colorStatus);
                StatusText.text = colorStatus;


                for (int k = 0; k < count; k++)
                {
                    BufferInt[k] = AvailableInt[k] - LoadInt[k];
                    //BufferString[k] = int.Parse(loadForecasts[i], System.Globalization.CultureInfo.InvariantCulture);
                    BufferString[k] = BufferInt[k].ToString(System.Globalization.CultureInfo.InvariantCulture);
                    //BufferString[k] = 
                    Debug.Log("BUFFER: " + BufferString[k]);

                }


                string[] LoadInfo = new string[count];
                for (int n = 0; n < count; n++)
                {
                    //LoadInfo[n] = "|  " + dates[n] + "  |  " + times[n] + "  |  Demand Forecast: " + loadForecasts[n] + " MW  |  Available Capacity " + declaredAvailabilities[n] + " MW |";
                    LoadInfo[n] = dates[n] + "     " + times[n] + "          " + loadForecasts[n] + "                     " + declaredAvailabilities[n] + "                             " + BufferString[n]; //+ "       ";
                    Debug.Log(LoadInfo[n]);
                }



                //int[] AvailableInt = new int[declaredAvailabilityMatches.Count];
                //int[] LoadInt = new int[declaredAvailabilityMatches.Count];
                //int[] BufferInt = new int[declaredAvailabilityMatches.Count];
                //string[] BufferString = new string[declaredAvailabilityMatches.Count];




                //string LoadInfo = dates[0];

                //string[] declaredAvailabilities = new string[declaredAvailabilityMatches.Count];
                //string[] loadForecasts = new string[loadForecastMatches.Count];
                //string[] maxAvailabilities = new string[maxAvailabilityMatches.Count];

                //for(int j = 5; j < count; j++)

                string AllLoadInfo = "\n" +
                    "_________________________________________________________________________" + "\n\n" +
                    " Date              Time      Load (MW)     Available Capacity (MW)       Available Buffer (MW)" + "\n" +
                    "_________________________________________________________________________" + "\n" +
                    LoadInfo[0] + "\n" +
                    LoadInfo[1] + "\n" +
                    LoadInfo[2] + "\n" +
                    LoadInfo[3] + "\n" +
                    LoadInfo[4] + "\n" +
                    LoadInfo[5] + "\n" +
                    LoadInfo[6] + "\n" +
                    LoadInfo[7] + "\n" +
                    LoadInfo[8] + "\n" +
                    LoadInfo[9] + "\n" +
                    LoadInfo[10] + "\n" +
                    LoadInfo[11] + "\n" +
                    LoadInfo[12] + "\n" +
                    LoadInfo[13] + "\n" +
                    LoadInfo[14] + "\n" +
                    LoadInfo[15] + "\n" +
                    LoadInfo[16] + "\n" +
                    LoadInfo[17] + "\n" +
                    LoadInfo[18] + "\n" +
                    LoadInfo[19] + "\n" +
                    LoadInfo[20] + "\n" +
                    LoadInfo[21] + "\n" +
                    LoadInfo[22] + "\n" +
                    LoadInfo[23]; // + "\n";



                Debug.Log(AllLoadInfo);
                ForecastText.text = AllLoadInfo;

                // Use HtmlAgilityPack to parse and extract specific data.
                HtmlDocument docLoad = new HtmlDocument();
                docLoad.LoadHtml(htmlContentLoad);

                // Use XPath to select specific elements.


                //HtmlNodeCollection nodes = docLoad.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[7]");
                // /html[1]/body[1]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[7]/td[4]
                //HtmlNodeCollection nodesLoad = docLoad.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[7]/td[4]");
                // /html[1]/body[1]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[7]/td[4]
                // / html[1] / body[1] / div[1] / div[4] / div[1] / table[1] / tbody[1] / tr[8] / td[4]
                //HtmlNodeCollection nodesLoad = docLoad.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[7]/td[4]");

                //HtmlNodeCollection nodesLoad = docLoad.DocumentNode.SelectNodes("/html[1]/body[1]/pre[1]");
                HtmlNodeCollection nodesLoad = docLoad.DocumentNode.SelectNodes("/html[1]");
                // /html[1]/body[1]/div[1]/div[5]/div[1]
                Debug.Log("Load Forecast web scraper");


                if (nodesLoad != null)
                {
                    Debug.Log("Load Forecast web scraper entered nodes not NULL");
                    foreach (HtmlNode node in nodesLoad)
                    {
                        // Extract and process data from the selected elements.
                        string extractedDataLoad = node.InnerText;
                        scrapedDataLoad = extractedDataLoad; // Store the scraped data
                        //public string scrapedData { get; private set; };
                        Debug.Log("Extracted Data For Load Forecast: " + scrapedDataLoad);
                        Debug.Log("Load Forecast web scraper entered nodes");
                        //public string scrapedData { get; private set; };
                    }
                }
                OnScrapingCompleteLoad?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }

    public string GetScrapedDataLoad()
    {
        return scrapedDataLoad;
    }
}


