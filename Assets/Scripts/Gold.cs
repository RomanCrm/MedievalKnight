using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
	int valueGold;

	public int ValueGold
	{
		get { return valueGold; }
		set{ valueGold = value; }
	}

	public Gold (int value)
	{
		ValueGold = value;
	}

}
