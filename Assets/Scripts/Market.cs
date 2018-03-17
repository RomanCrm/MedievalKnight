using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market: MonoBehaviour
{
	public void AddClothes(GameObject marketGrid, GameObject container, List<Element> elements)
	{
		for (int i = 0; i < elements.Count; i++)
		{
			GameObject obj = Instantiate (container);
			obj.tag = elements [i].Tag.ToString();
			obj.transform.SetParent (marketGrid.transform.GetChild (i).transform);
			obj.GetComponent<Image> ().sprite = Resources.Load<Sprite> (elements [i].Path);
			obj.AddComponent<Button> ().onClick.AddListener(() => Buy(obj.tag));
		}
	}

	public void Buy(string tagClicked)
	{
		PlayerPrefs.SetString ("ClickedTag", tagClicked);
		PlayerPrefs.SetInt ("Clicked", 0);
		PlayerPrefs.SetInt ("Clicked", 1);
	}

}
