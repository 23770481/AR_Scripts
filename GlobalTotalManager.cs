using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
using TMPro;

public class GlobalTotalManager : MonoBehaviour
{
    // Create a public static instance for the Singleton pattern
    public static GlobalTotalManager Instance;
    public TMP_Text TotalPriceText;

    // Create a public variable to store the running total
    public double runningTotal = 0;
    public int printerF = 0;
    public int coffeeF = 0;
    public int grillF = 0;
    public int heaterF = 0;
    public int fanF = 0;
    //private bool printerF = false;
    private double PrinterPrice;
    private double CoffeePrice;
    private double FanPrice;
    private double GrillPrice;
    private double HeaterPrice;


    private void Awake()
    {
        // Ensure there's only one instance of this script
        //Debug.Log("Running Total: " + runningTotal);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PrinterFound()
    {

        if (printerF == 0)
        {
            printerF = 1;
            runningTotal += PrinterPrice;
            string TotalCostStr = runningTotal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string TotalInfo = "Combined Total Cost (Per Hour):  R" + TotalCostStr + "\n\n";
            TotalPriceText.text = TotalInfo;
        }
        //printerF = 1;
        Debug.Log("PRINTER HAS BEEN FOUND: " + printerF);
        //Debug.Log("PRINTER HAS BEEN FOUND: " + printerFF);
        Debug.Log("Running Total: " + runningTotal);
    }

    public void CoffeeFound()
    {
        if (coffeeF == 0)
        {
            coffeeF = 1;
            runningTotal += CoffeePrice;
            string TotalCostStr = runningTotal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string TotalInfo = "Combined Total Cost (Per Hour):  R" + TotalCostStr + "\n\n";
            TotalPriceText.text = TotalInfo;
        }
        Debug.Log("Running Total: " + runningTotal);
    }

    public void FanFound()
    {
        if (fanF == 0)
        {
            fanF = 1;
            runningTotal += FanPrice;
            string TotalCostStr = runningTotal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string TotalInfo = "Combined Total Cost (Per Hour):  R" + TotalCostStr + "\n\n";
            TotalPriceText.text = TotalInfo;
        }
        Debug.Log("Running Total: " + runningTotal);
    }

    public void HeaterFound()
    {
        if (heaterF == 0)
        {
            heaterF = 1;
            runningTotal += HeaterPrice;
            string TotalCostStr = runningTotal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string TotalInfo = "Combined Total Cost (Per Hour):  R" + TotalCostStr + "\n\n";
            TotalPriceText.text = TotalInfo;
        }
        Debug.Log("Running Total: " + runningTotal);
    }

    public void GrillFound()
    {
        if (grillF == 0)
        {
            grillF = 1;
            runningTotal += GrillPrice;
            string TotalCostStr = runningTotal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string TotalInfo = "Combined Total Cost (Per Hour):  R" + TotalCostStr + "\n\n";
            TotalPriceText.text = TotalInfo;
        }
        Debug.Log("Running Total: " + runningTotal);
    }


    public void GetPrinterPrice(double GetPrice)
    {
        PrinterPrice = GetPrice;
    }

    public void GetCoffeePrice(double GetPrice)
    {
        CoffeePrice = GetPrice;
    }

    public void GetFanPrice(double GetPrice)
    {
        FanPrice = GetPrice;
    }

    public void GetGrillPrice(double GetPrice)
    {
        GrillPrice = GetPrice;
    }

    public void GetHeaterPrice(double GetPrice)
    {
        HeaterPrice = GetPrice;
    }
    // Function to update the running total and print it to the console
    //public void updatetotal(double valuetoadd)
    //{
    //    runningtotal += valuetoadd;
    //    debug.log("running total: " + runningtotal);
    //}
}
