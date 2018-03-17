using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptMarket : MonoBehaviour
{
	Text txt;

	void Start()
	{
		txt = gameObject.GetComponent<Text> ();
	}

	void Update ()
	{
		txt.color = new Color (txt.color.r, txt.color.g, txt.color.b, Mathf.PingPong(Time.time/2.5f, 1.0f));
	}
}
