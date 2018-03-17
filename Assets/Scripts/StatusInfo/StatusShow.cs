using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatusShow : MonoBehaviour
{
	public GameObject statusPanel;

	public void Click()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (statusPanel.activeSelf)
			{
				statusPanel.SetActive (false);
			}
			else
			{
				statusPanel.SetActive (true);
			}
		}
	}

}
