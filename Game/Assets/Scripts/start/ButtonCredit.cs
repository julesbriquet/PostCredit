using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonCredit : MonoBehaviour, ISelectHandler, IDeselectHandler {

	public MenuStart menu;

	#region IDeselectHandler implementation

	void IDeselectHandler.OnDeselect (BaseEventData eventData)
	{
		menu.SetNormal();
	}

	#endregion

	#region ISelectHandler implementation
	void ISelectHandler.OnSelect (BaseEventData eventData)
	{
		menu.SetCredit();
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
