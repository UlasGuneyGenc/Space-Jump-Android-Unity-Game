using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Transform background1;
    public Transform background2;

    private bool changeBackground = true;

    public Transform cam;
    private float currentHeight = 9.928f;


    private void Update()
    {
       
        if(currentHeight < cam.position.y)
        {
            if (changeBackground)
            {
                background1.localPosition = new Vector3(0, background1.localPosition.y + (9.928f * 2.0f), 10);
            }
            else
            {
                background2.localPosition = new Vector3(0, background2.localPosition.y + (9.928f * 2.0f), 10);
            }
            currentHeight += 9.928f;
            changeBackground = !changeBackground;
        }

        if (currentHeight > cam.position.y + 9.928)
        {
            if (changeBackground)
            {
                background2.localPosition = new Vector3(0, background2.localPosition.y - (9.928f * 2.0f), 10);
            }
            else
            {
                background1.localPosition = new Vector3(0, background1.localPosition.y - (9.928f * 2.0f), 10);
            }
            currentHeight -= 9.928f;
            changeBackground = !changeBackground;
        }


    }


}
