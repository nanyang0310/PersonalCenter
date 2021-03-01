using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace UIWidgetsSamples
{

    public class CustomSecondTableCell : MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IPointerClickHandler
    {
        public UnityEvent  CellClicked = new UnityEvent();

        public void OnPointerUp(PointerEventData eventData)
        {
            CellClicked.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}
