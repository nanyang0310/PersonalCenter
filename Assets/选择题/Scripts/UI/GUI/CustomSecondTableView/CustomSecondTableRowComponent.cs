using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UIWidgets;
using UnityEngine.UI;

namespace UIWidgetsSamples
{
    public class CustomSecondTableRowComponent : ListViewItem, IResizableItem, IViewData<CustomSecondTableRow>
    {
        public override Graphic[] GraphicsBackground
        {
            get
            {
                return new Graphic[] { Name1, Name2, Name3, Name4, Name5, Name6, Name7, Name8, Name9, Name10 };
            }
        }

        public override Graphic[] GraphicsForeground
        {
            get { return new Graphic[] {}; }
        }

        [SerializeField] public TextMeshProUGUI Name1;
        [SerializeField] public TextMeshProUGUI Name2;
        [SerializeField] public TextMeshProUGUI Name3;
        [SerializeField] public TextMeshProUGUI Name4;
        [SerializeField] public TextMeshProUGUI Name5; 
        [SerializeField] public TextMeshProUGUI Name6;
        [SerializeField] public TextMeshProUGUI Name7;
        [SerializeField] public TextMeshProUGUI Name8;
        [SerializeField] public TextMeshProUGUI Name9;
        [SerializeField] public TextMeshProUGUI Name10;

        private CustomSecondTableRow Item;

        public GameObject[] ObjectsToResize
        {
            get
            {
                return new GameObject[]
                {
                    Name1.transform.parent.gameObject,
                    Name2.transform.parent.gameObject,
                    Name3.transform.parent.gameObject,
                    Name4.transform.parent.gameObject, 
                    Name5.transform.parent.gameObject, 
                    Name6.transform.parent.gameObject,
                    Name7.transform.parent.gameObject,
                    Name8.transform.parent.gameObject,
                    Name9.transform.parent.gameObject,
                    Name10.transform.parent.gameObject,
                };
            }
        }
        public void SetData(CustomSecondTableRow item)
        {
            Item = item;
            Name1.text = Item.Name1;
            Name2.text = Item.Name2;
            Name3.text = Item.Name3;
            Name4.text = Item.Name4;
            Name5.text = Item.Name5;
            Name6.text = Item.Name6;
            Name7.text = Item.Name7;
            Name8.text = Item.Name8;
            Name9.text = Item.Name9;
            Name10.text = Item.Name10;
        }

        public void CellClicked(string cellName)
        {
            Debug.Log(string.Format("clicked row {0}, cell {1}", Index, cellName));
            switch (cellName)
            {
                case "Name1":
                    Debug.Log("cell value: " + Item.Name1);
                    break;
                case "Name2":
                    Debug.Log("cell value: " + Item.Name2);
                    break;
                case "Name3":
                    Debug.Log("cell value: " + Item.Name3);
                    break;
                case "Name4":
                    Debug.Log("cell value: " + Item.Name4);
                    break;
                case "Name5":
                    Debug.Log("cell value: " + Item.Name5);
                    break;
                case "Name6":
                    Debug.Log("cell value: " + Item.Name6);
                    break;
                case "Name7":
                    Debug.Log("cell value: " + Item.Name7);
                    break;
                case "Name8":
                    Debug.Log("cell value: " + Item.Name8);
                    break;
                case "Name9":
                    Debug.Log("cell value: " + Item.Name9);
                    break;
                case "Name10":
                    Debug.Log("cell value: " + Item.Name10);
                    break;
                default:
                    Debug.Log("cell value: <unknown cell>");
                    break;
            }
        }
        
    }
}