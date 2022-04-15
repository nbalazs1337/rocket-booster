using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonsClicked : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool isPressed = false;
    public GameObject Up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            //thrust
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
    
}
