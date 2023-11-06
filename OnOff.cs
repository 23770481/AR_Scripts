using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public GameObject Total;
    public void whenButtonClicked()
    {
        if (Total.activeInHierarchy == true)
            Total.SetActive(false);
        else
            Total.SetActive(true);
    }
}
