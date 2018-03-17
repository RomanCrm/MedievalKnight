using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnterElement : MonoBehaviour, IPointerEnterHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
//	public static GameObject itemBeingDragged;
//	Vector3 startPosition;
//	Transform startParent;

	bool isNot;

	public void OnPointerEnter(PointerEventData eventData)
	{
		PlayerPrefs.SetString ("EnterTag", tag);
	}
		
//	#region IBeginDragHandler implementation
//	public void OnBeginDrag (PointerEventData eventData)
//	{
//		if (transform.parent.tag != "Market")
//		{
//			itemBeingDragged = gameObject;
//			startPosition = transform.position;
//			startParent = transform.parent;
//			GetComponent<CanvasGroup> ().blocksRaycasts = false;
//
//			isNot = true;
//		}
//		else
//		{
//			GetComponent<CanvasGroup> ().blocksRaycasts = false;
//		}
//	}
//	#endregion
//
//	#region IDragHandler implementation
//	public void OnDrag (PointerEventData eventData)
//	{
//		if (isNot)
//		{
//			transform.position = Input.mousePosition;
//		}
//	}
//	#endregion
//
//	#region IEndDragHandler implementation
//	public void OnEndDrag (PointerEventData eventData)
//	{
//		if (isNot)
//		{
//			itemBeingDragged = null;
//			GetComponent<CanvasGroup> ().blocksRaycasts = true;
//			if (transform.parent == startParent)
//			{
//				transform.position = startPosition;
//			}
//		}
//	}
//	#endregion
}
