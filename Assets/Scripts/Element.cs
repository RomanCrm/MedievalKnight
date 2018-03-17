using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyParts
{
	Head,
	Torso,
	Leg,
	Foot
}

public class Element : MonoBehaviour
{
	public int Price { get; set; }
	public int HP { get; set; }
	public int Armor { get; set; }
	public int Strength { get; set; }

	public BodyParts Part { get; set; }
	public bool IsSet { get; set; }

	public object Tag { get; set; }
	public string Path { get; set; }
}
