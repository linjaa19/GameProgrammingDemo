using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItemTest()
    {
        if (GlobalVariables.score >= 4)
        {
            GlobalVariables.score -= 4;
        }

    }
}
