/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    
    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        //CreateCircle(new Vector2(200, 200));

        List<int> valueList = new List<int>() { 5, 12, 45, 24, 31, 48, 35, 23, 41, 43, 19, 29, 5, 12, 45, 24, 31, 48, 35, 23, 41, 43, 19, 29 };
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 50f; //100
        float xSize = 50f; //50

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

            if(lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        //rectTransform.anchoredPosition = dotPositionA;
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
*/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Net.Http;
using System.Threading.Tasks;

using HtmlAgilityPack; // Import HtmlAgilityPack namespace
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.Events;
//using Vuforia;
using TMPro;

public class Window_Graph : MonoBehaviour
{
    public string scrapedDataLoad { get; private set; }
    public event Action OnScrapingCompleteLoad;
    //public TMP_Text ForecastText;
    private int count;

    [SerializeField]
    private string scrapeUrlLoad = "https://example.com";
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;

    //private void Awake()
    //{
    //    graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    //    //CreateCircle(new Vector2(200, 200));

    //    List<int> valueList = new List<int>() { 5, 12, 45, 24, 31, 48, 35, 23, 41, 43, 19, 29, 5, 12, 45, 24, 31, 48, 35, 23, 41, 43, 19, 29 };
    //    ShowGraph(valueList);
    //}

    private async void Start()
    {
        // Start the web scraping process.
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
                Debug.Log("HTML Content From Window_Graph Script NBNBNBNBNB: " + htmlContentLoad);



                // Define regular expression patterns to extract various data
                string timestampPattern = "\"Timestamp\":\"(.*?)T(.*?)\"";
                string declaredAvailabilityPattern = "\"DeclaredAvailabilty\":(\\d+\\.\\d+)";
                string loadForecastPattern = "\"LoadForecast\":(\\d+\\.\\d+)";
                string maxAvailabilityPattern = "\"MaxAvailability\":(\\d+\\.\\d+)";
                string colorPattern = "\"Color\":\"(.*?)\"";
                string directionPattern = "\"Direction\":\"(.*?)\"";
                string directionIdPattern = "\"DirectionId\":(\\d+)";

                // Create regular expression objects
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

                // Create arrays to store the extracted data
                string[] dates = new string[timestampMatches.Count];
                string[] times = new string[timestampMatches.Count];
                string[] declaredAvailabilities = new string[declaredAvailabilityMatches.Count];
                string[] loadForecasts = new string[loadForecastMatches.Count];
                string[] maxAvailabilities = new string[maxAvailabilityMatches.Count];
                string[] colors = new string[colorMatches.Count];
                string[] directions = new string[directionMatches.Count];
                string[] directionIds = new string[directionIdMatches.Count];

                //string originalString = "HelloWorld";
                //string firstFiveLetters = originalString.Substring(0, Math.Min(5, originalString.Length));


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
                }

                for (int i = 0; i < loadForecastMatches.Count; i++)
                {
                    loadForecasts[i] = loadForecastMatches[i].Groups[1].Value;
                    loadForecasts[i] = loadForecasts[i].Substring(0, Math.Min(5, loadForecasts[i].Length));
                }

                for (int i = 0; i < maxAvailabilityMatches.Count; i++)
                {
                    maxAvailabilities[i] = maxAvailabilityMatches[i].Groups[1].Value;
                    maxAvailabilities[i] = maxAvailabilities[i].Substring(0, Math.Min(5, maxAvailabilities[i].Length));
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




                //string[] LoadInfo = new string[count];
                //for (int n = 0; n < count; n++)
                //{
                //    //LoadInfo[n] = "|  " + dates[n] + "  |  " + times[n] + "  |  Demand Forecast: " + loadForecasts[n] + " MW  |  Available Capacity " + declaredAvailabilities[n] + " MW |";
                //    LoadInfo[n] = "|  " + dates[n] + "  |  " + times[n] + "  |  Demand Forecast: " + loadForecasts[n] + " MW  |  Available Capacity " + declaredAvailabilities[n] + " MW |";
                //    Debug.Log(LoadInfo[n]);
                //}


                string[] LoadInfo = new string[count];
                for (int n = 0; n < count; n++)
                {
                    //LoadInfo[n] = "|  " + dates[n] + "  |  " + times[n] + "  |  Demand Forecast: " + loadForecasts[n] + " MW  |  Available Capacity " + declaredAvailabilities[n] + " MW |";
                    LoadInfo[n] = dates[n] + "     " + times[n] + "               " + loadForecasts[n] + "                      " + declaredAvailabilities[n]; //+ "       ";
                    Debug.Log(LoadInfo[n]);
                }

                //string LoadInfo = dates[0];

                //string[] declaredAvailabilities = new string[declaredAvailabilityMatches.Count];
                //string[] loadForecasts = new string[loadForecastMatches.Count];
                //string[] maxAvailabilities = new string[maxAvailabilityMatches.Count];

                //for(int j = 5; j < count; j++)

                string AllLoadInfo = "\n" +
                    "__________________________________________________________" + "\n\n" +
                    " Date              Time      Load forecast (MW)    Available Capacity (MW)" + "\n" +
                    "__________________________________________________________" + "\n" +
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
                    LoadInfo[16]; //+ "\n"; +
                                  //LoadInfo[17] + "\n" +
                                  //LoadInfo[18] + "\n" +
                                  //LoadInfo[19] + "\n" +
                                  //LoadInfo[20] + "\n" +
                                  //LoadInfo[21] + "\n" +
                                  //LoadInfo[22] + "\n" +
                                  //LoadInfo[23] + "\n";



                Debug.Log(AllLoadInfo);
                //ForecastText.text = AllLoadInfo;

                graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
                labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
                labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

                dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
                dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();

                //CreateCircle(new Vector2(200, 200));

                //List<int> valueList = new List<int>() { 5, 12, 45, 24, 31, 48, 35, 23, 41, 43, 19, 29, 5, 12, 45, 24, 31, 48, 35, 23, 41, 43, 19, 29 };


                // Convert the strings to integers and add them to the valueList
                List<int> valueList = new List<int>();
                List<int> availableList = new List<int>();
                List<int> maxList = new List<int>();
                //List<int> valueList = new List<int>();

                for (int i = 0; i < loadForecasts.Length; i++)
                {
                    int parsedValue;
                    if (int.TryParse(loadForecasts[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedValue))
                    {
                        //parsedValue = parsedValue * 0.001;
                        valueList.Add(parsedValue);
                        //availableList.Add(parsedValue);
                        Debug.Log("ADDED TO LIST VALUE_LIST: " + parsedValue);
                    }
                    else
                    {
                        Debug.LogError("Failed to convert string to int at index " + i);
                        valueList.Add(25000);
                    }
                    Debug.Log(i);
                }
                //availableList[0] = 10000;

                Debug.Log("DECLARED AVAILABILITIES");

                for (int i = 0; i < declaredAvailabilities.Length; i++)
                {


                    int parsedAvailable;

                    if (int.TryParse(declaredAvailabilities[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedAvailable))
                    {
                        //parsedValue = parsedValue * 0.001;
                        availableList.Add(parsedAvailable);
                        Debug.Log("ADDED TO LIST VALUE_LIST: " + parsedAvailable);
                    }
                    else
                    {
                        Debug.LogError("Failed to convert string to int at index " + i);
                        availableList.Add(25000);
                    }
                    Debug.Log(i);
                }

                //maxAvailabilities

                for (int i = 0; i < maxAvailabilities.Length; i++)
                {


                    int maxAvailable;

                    if (int.TryParse(maxAvailabilities[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out maxAvailable))
                    {
                        //parsedValue = parsedValue * 0.001;
                        maxList.Add(maxAvailable);
                        Debug.Log("ADDED TO LIST VALUE_LIST: " + maxAvailable);
                    }
                    else
                    {
                        Debug.LogError("Failed to convert string to int at index " + i);
                        maxList.Add(25000);
                    }
                    Debug.Log(i);
                }


                ShowGraph(valueList, availableList, maxList, times);
                //ShowGraph(availableList, times);

                //declaredAvailabilities///////////////////////










                // Use HtmlAgilityPack to parse and extract specific data.
                HtmlDocument docLoad = new HtmlDocument();
                docLoad.LoadHtml(htmlContentLoad);


                HtmlNodeCollection nodesLoad = docLoad.DocumentNode.SelectNodes("/html[1]");
                // /html[1]/body[1]/div[1]/div[5]/div[1]
                //Debug.Log("Load Forecast web scraper");


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

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        //rectTransform.sizeDelta = new Vector2(11, 11); //original dots
        rectTransform.sizeDelta = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList, List<int> availableList, List<int> maxList, string[] times)
    {
        //int lineColor;
        float graphHeight = graphContainer.sizeDelta.y;
        //float yMaximum = 50f; //100
        //float yMaximum = 30000f; //100
        float xSize = 80f; //50

        float yMaximum = valueList[0]; //;
        float yMinimum = valueList[0];

        foreach (int value in valueList)
        {
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }

        }

        foreach (int value in availableList)
        {
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }
        }

        foreach (int value in maxList)
        {
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }
        }

        yMaximum = yMaximum + ((yMaximum - yMinimum) * 0.1f); //0.2f
        yMinimum = yMinimum - ((yMaximum - yMinimum) * 0.1f); //0.2f

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            //float xPosition = xSize + i * xSize; //ORIGINAL
            //float xPosition = (xSize * 0.5f) + i * xSize;
            float xPosition = (xSize * 0f) + i * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, 1);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            //labelX.anchoredPosition = new Vector2(xPosition, -20f); //ORIGINAL
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
            //labelX.GetComponent<TMPro.TextMeshProUGUI>().text = i.ToString();

            //string[] times = new string[timestampMatches.Count];
            //times[i] = times[i].Substring(0, Math.Min(5, times[i].Length));
            string[] timesHoursOnly = new string[24];
            timesHoursOnly[i] = times[i].Substring(0, Math.Min(2, times[i].Length));
            labelX.GetComponent<TMPro.TextMeshProUGUI>().text = timesHoursOnly[i];


            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, 0f);


            Debug.Log(times[i]);
        }


        GameObject lastCircleGameObjectAV = null;
        for (int i = 0; i < availableList.Count; i++)
        {
            //float xPosition = xSize + i * xSize; //ORIGINAL
            //float xPosition = (xSize * 0.5f) + i * xSize;
            float xPosition = (xSize * 0f) + i * xSize;
            float yPosition = ((availableList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

            if (lastCircleGameObjectAV != null)
            {
                CreateDotConnection(lastCircleGameObjectAV.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, 2);
            }
            lastCircleGameObjectAV = circleGameObject;

            //RectTransform labelX = Instantiate(labelTemplateX);
            //labelX.SetParent(graphContainer);
            //labelX.gameObject.SetActive(true);
            ////labelX.anchoredPosition = new Vector2(xPosition, -20f); //ORIGINAL
            //labelX.anchoredPosition = new Vector2(xPosition, -20f);
            ////labelX.GetComponent<TMPro.TextMeshProUGUI>().text = i.ToString();

            ////string[] times = new string[timestampMatches.Count];
            ////times[i] = times[i].Substring(0, Math.Min(5, times[i].Length));
            //string[] timesHoursOnly = new string[24];
            //timesHoursOnly[i] = times[i].Substring(0, Math.Min(2, times[i].Length));
            //labelX.GetComponent<TMPro.TextMeshProUGUI>().text = timesHoursOnly[i];
            //Debug.Log(times[i]);
        }



        GameObject lastCircleGameObjectMax = null;
        for (int i = 0; i < maxList.Count; i++)
        {
            //float xPosition = xSize + i * xSize; //ORIGINAL
            //float xPosition = (xSize * 0.5f) + i * xSize;
            float xPosition = (xSize * 0f) + i * xSize;
            float yPosition = ((maxList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

            if (lastCircleGameObjectMax != null)
            {
                CreateDotConnection(lastCircleGameObjectMax.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, 3);
            }
            lastCircleGameObjectMax = circleGameObject;

        }



        //int separatorCount = 10; //ORIGINAL
        int separatorCount = 9;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            //labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight); //original
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            //labelY.GetComponent<TMPro.TextMeshProUGUI>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();

            labelY.GetComponent<TMPro.TextMeshProUGUI>().text = Mathf.RoundToInt(yMinimum + (normalizedValue * (yMaximum - yMinimum))).ToString();


            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(0f, normalizedValue * graphHeight);

        }
    }





    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, int lineColor)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        //gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f); //for white lines

        if (lineColor == 1)
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.8f);
        }
        else if (lineColor == 2)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.8f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(0f, 0f, 1f, 0.8f);
        }
        
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        //rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.sizeDelta = new Vector2(distance, 5f); //SET LINE THICKNESS
        //rectTransform.anchoredPosition = dotPositionA;
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
