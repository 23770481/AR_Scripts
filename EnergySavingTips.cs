using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using Vuforia;
using TMPro;

//public class EnergySavingTips : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}



public class EnergySavingTips : MonoBehaviour
{
    public TMP_Text TipsText;
    
    

    private void Start()
    {
        // Generate a random integer between 0 and 9 (inclusive)
        int tipsNum = 4;
        int randomNumber = Random.Range(0, tipsNum);
        string[] TipArray = new string[tipsNum];

        Debug.Log("Random Number: " + randomNumber);

        TipArray[0] = "Washing machines can account for up to 11.8% of household electricity bills. " +
            "Skipping the washing machine's pre-wash cycle uses up to 20% less electricity\n\n" +
            "The average South African Household can save up to: R52 p/m";

        TipArray[1] = "Lighting accounts for about 10% household electricity bills. " +
            "You can save up to 75% of that energy by replacing incandescent bulbs with compact fluorescent bulbs (CFLs).\n\n" +
            "The average South African Household can save up to: " +
            "R165 p/m";

        TipArray[2] = "Hot water geysers can account for up to 40% of a household’s electricity bill. " +
            "Households can save between 6 % and 29 % of energy used by geysers by simply turning the appliance off just before using hot water, and then switching it on again about 2 hours before it’s needed.\n\n" +
            "The average South African Household can save up to: " +
            "R255 p/m";

        TipArray[3] = "Many appliances like TVs, microwaves, chargers, monitors and computers continue to use electricity while on standby, even when they are switched off and not is use. " +
            "This standby power accounts for nearly 10% of a households energy consumption\n\n" +
            "The average South African Household can save up to: " +
            "R220 p/m";

        if (randomNumber == 0)
        {
            TipsText.text = "The average South African household uses around 900 kWh of electricity per month, and spends approximately R2 200 on electricity per month.\n\n" +
                TipArray[0];
                
        }

        else if (randomNumber == 1)
        {
            TipsText.text = "The average South African household uses around 900 kWh of electricity per month, and spends approximately R2 200 on electricity per month.\n\n" +
                TipArray[1];

        }

        else if (randomNumber == 2)
        {
            TipsText.text = "The average South African household uses around 900 kWh of electricity per month, and spends approximately R2 200 on electricity per month.\n\n" +
                TipArray[2];

        }

        else if (randomNumber == 3)
        {
            TipsText.text = "The average South African household uses around 900 kWh of electricity per month, and spends approximately R2 200 on electricity per month.\n\n" +
                TipArray[3];

        }

        else
        {
            TipsText.text = "The average South African household uses around 900 kWh of electricity per month, and spends approximately R2 200 on electricity per month.\n\n";
        }


        //  Reduce your electricity account by skipping the washing machine's pre-wash cycle if your clothes are not particularly dirty. 
        //  This will use up to 20% less electricity.

        //  Around 80% of the energy used to wash clothes comes from heating the water. 
        //  Using warm or cool water will save energy and get clothes just as clean.

        //  Lighting makes up about 10 percent of home energy costs. Save up to 75 percent of that energy by replacing incandescent bulbs with compact fluorescent bulbs (CFLs). 
        //  They also last up to 25 times longer, saving money on replacements.  
        //  only about 10 to 15 percent of the electricity that incandescent lights consume results in light -- the rest is turned into heat.

        //  
        //  
        //  

        //  Many appliances like TVs, microwaves, chargers, monitors and computers continue to use electricity while on standby, which typically amounts to hours every day. 
        //  To save energy and up to 10% of your energy bills, unplug electronics when not in use.

        //  The average South African household uses around 900 kWh of electricity per month, and spends approximately R2 200 on electricity per month.




    }
}

