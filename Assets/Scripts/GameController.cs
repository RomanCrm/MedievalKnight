using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Thousands
{
	OneThousand = 1000
}

public enum Hundreds
{
	OneHundred = 100
}

public class GameController : MonoBehaviour
{
	// common
	public GameObject container;
	////////////////////////////

	// inventory
	public GameObject inventoryPanel;
	public GameObject gridPanel;

	List<Element> inventoryItems = new List<Element>();

	public Text valueGoldTxt;
	Gold gold = new Gold ((int)Thousands.OneThousand);
	////////////////////////////

	// market
	public Text priceValTxt;
	public Text hpValTxt;
	public Text armorValTxt;
	public Text strengthValTxt;

	public GameObject marketPanel;
	public GameObject marketGrid;

	Market market = new Market ();
	List<Element> elements = new List<Element>();
	bool isAdded;
	////////////////////////////

	// stats
	public Text armorValText;
	int armor;
	public Text strengthValText;
	int strength;
	public Text hpPoints;
	int hp = (int)Hundreds.OneHundred;

	public Slider sliderHp;
	////////////////////////////

	// Character
	List<GameObject> clothesPers = new List<GameObject> (); // all child elements of the character (clothes, bones and so on...)
	////////////////////////////

	void Start ()
	{
		GetCharacterParts ();

		sliderHp.onValueChanged.AddListener (delegate {SliderValueChaneged();});

		InitializePlayerPrefs ();
		AddClothes (elements);
	}

	void SliderValueChaneged()
	{
		
	}

	// Getting the body parts of our character 
	void GetCharacterParts()
	{
		for(int i = 0; i < gameObject.transform.childCount; i++)
		{
			clothesPers.Add (gameObject.transform.GetChild (i).gameObject);
		}
	}

	void InitializePlayerPrefs()
	{
		PlayerPrefs.SetInt ("GoldValue", gold.ValueGold);
		valueGoldTxt.text = PlayerPrefs.GetInt ("GoldValue").ToString();

		PlayerPrefs.SetInt ("HP", hp);
		PlayerPrefs.SetInt ("Armor", armor);
		PlayerPrefs.SetInt ("Strength", strength);
		hpPoints.text = PlayerPrefs.GetInt ("HP").ToString();
		armorValText.text = PlayerPrefs.GetInt ("Armor").ToString();
		strengthValText.text = PlayerPrefs.GetInt ("Strength").ToString();
	}

	void Update ()
	{
		Inventory ();
		Market ();
		EnterElementMarket ();
		Quit ();
	}

	void Inventory()
	{
		if (Input.GetKeyDown (KeyCode.I))
		{
			if(inventoryPanel.activeSelf)
			{
				inventoryPanel.SetActive(false);

				for (int i = 0; i < inventoryItems.Count; i++)
				{
					if (gridPanel.transform.GetChild (i).transform.childCount != 0)
					{
						Destroy (gridPanel.transform.GetChild (i).transform.GetChild (0).gameObject);
					}
				}
			}
			else
			{
				inventoryPanel.SetActive (true);

				for (int i = 0; i < inventoryItems.Count; i++)
				{
					GameObject obj = Instantiate (container);
					obj.tag = inventoryItems [i].Tag.ToString ();
					obj.transform.SetParent (gridPanel.transform.GetChild (i).transform);	
					obj.GetComponent<Image> ().sprite = Resources.Load<Sprite> (inventoryItems [i].Path);
					obj.AddComponent<Button> ().onClick.AddListener (() => DressUpSelected (obj.tag));
				}
			}
		}
	}
		
	/// Dresses selected clothes
	void DressUpSelected(string tagSelected)
	{
		// looking for an object on the character
		foreach (GameObject obj in clothesPers)
		{
			// found
			if (obj.tag == tagSelected)
			{
				// Looking for the selected object in the inventory
				foreach(Element element in inventoryItems)
				{
					// found
					if(element.Tag.ToString() == obj.tag)
					{
						// Loking for an index in the character
						int idxCurObj = clothesPers.IndexOf (obj);
						// Which part of the body
						switch (element.Part)
						{
						case BodyParts.Head:
							{
								SetSelectedClothes (BodyParts.Head, idxCurObj);
							} break;
						case BodyParts.Torso:
							{
								SetSelectedClothes (BodyParts.Torso, idxCurObj);
							} break;
						case BodyParts.Leg:
							{
								SetSelectedClothes (BodyParts.Leg, idxCurObj);
							} break;
						case BodyParts.Foot:
							{
								SetSelectedClothes (BodyParts.Foot, idxCurObj);
							} break;
						}
					}
				}
			}
		}
	}

	/// Sets the selected clothes. Enables/disables previous/new
	void SetSelectedClothes(BodyParts part, int idxCurObj)
	{
		int idxPart = 0;

		foreach (Element partElem in inventoryItems)
		{
			if (partElem.Part == part)
			{
				foreach (GameObject objPart in clothesPers)
				{
					if (objPart.tag == partElem.Tag.ToString ())
					{
						if (objPart.activeSelf)
						{
							objPart.SetActive (false);
							idxPart = clothesPers.IndexOf (objPart);

							if (hp > 100 && strength > 0 && armor > 0)
							{
								hp -= partElem.HP;
								armor -= partElem.Armor;
								strength -= partElem.Strength;

								if (hp < sliderHp.maxValue)
								{
									sliderHp.maxValue -= partElem.HP;
									sliderHp.value = sliderHp.maxValue;
								}
							}
						}
						else
						{
							objPart.SetActive (false);
						}
					}
				}
			}
		}

		if (idxCurObj == idxPart)
		{
			clothesPers[idxCurObj].SetActive (false);
		}
		else
		{
			clothesPers[idxCurObj].SetActive (true);

			foreach (Element element in inventoryItems)
			{
				if (element.Tag.ToString() == clothesPers [idxCurObj].tag) 
				{
					hp += element.HP;
					armor += element.Armor;
					strength += element.Strength;

					if (hp > sliderHp.maxValue)
					{
						sliderHp.maxValue = hp;
					}
					sliderHp.value = hp;

					break;
				}
			}
		}

		PlayerPrefs.SetInt ("HP", hp);
		PlayerPrefs.SetInt ("Armor", armor);
		PlayerPrefs.SetInt ("Strength", strength);

		hpPoints.text = PlayerPrefs.GetInt ("HP").ToString();
		armorValText.text = PlayerPrefs.GetInt ("Armor").ToString ();
		strengthValText.text = PlayerPrefs.GetInt ("Strength").ToString();
	}

	///  Opens/Closes the market window
	void Market()
	{
		if (Input.GetKeyDown (KeyCode.M))
		{
			if (marketPanel.activeSelf)
			{
				marketPanel.SetActive (false);
			}
			else
			{
				marketPanel.SetActive (true);
				if (!isAdded)
				{
					market.AddClothes (marketGrid, container, elements);
					isAdded = true;
				}
			}
		}
			
		bool isHave = false;

		// clicked = 1, didn`t click = 0, purchasing
		if (PlayerPrefs.GetInt ("Clicked") == 1)
		{
			if (inventoryItems.Count < elements.Count)
			{
				foreach (Element element in elements)
				{
					if (element.Tag.ToString () == PlayerPrefs.GetString ("ClickedTag"))
					{
						foreach (Element item in inventoryItems)
						{
							if (item.Tag.ToString () == PlayerPrefs.GetString ("ClickedTag"))
							{
								isHave = true;
								break;
							}
						}
						if (!isHave)
						{
							if (gold.ValueGold > element.Price)
							{	
								inventoryItems.Add (element);
								gold.ValueGold -= element.Price;
								PlayerPrefs.SetInt ("GoldValue", gold.ValueGold);
								valueGoldTxt.text = PlayerPrefs.GetInt ("GoldValue").ToString ();	

								isHave = false;

								break;
							}
						}
					}
				}
				PlayerPrefs.SetInt ("Clicked", 0);
			}
		}
	}

	/// changes the values when hovering the mouse
	void EnterElementMarket()
	{
		if (marketPanel.activeSelf)
		{
			string owrTag;
			for (int i = 0; i < elements.Count; i++)
			{
				owrTag = elements [i].Tag.ToString();
				if (owrTag == PlayerPrefs.GetString ("EnterTag"))
				{
					priceValTxt.text = elements [i].Price.ToString ();
					hpValTxt.text = elements [i].HP.ToString ();
					armorValTxt.text = elements [i].Armor.ToString ();
					strengthValTxt.text = elements [i].Strength.ToString ();
				}
			}
		}
	}

	/// Adds clothes
	void AddClothes(List<Element> elements)
	{
		elements.Add (new Element{ Path = "Icons/Helmet1", Armor = 2, HP = 5, Price = 5, Strength = 1, Tag = "Helmet1", Part = BodyParts.Head});
		elements.Add (new Element{ Path = "Icons/Helmet2", Armor = 3, HP = 5, Price = 7, Strength = 2, Tag = "Helmet2", Part = BodyParts.Head});

		elements.Add (new Element{ Path = "Icons/Body1", Armor = 4, HP = 40, Price = 15, Strength = 10, Tag = "Body1", Part = BodyParts.Torso});
		elements.Add (new Element{ Path = "Icons/Body2", Armor = 9, HP = 55, Price = 15, Strength = 15, Tag = "Body2", Part = BodyParts.Torso});

		elements.Add (new Element{ Path = "Icons/Boots1", Armor = 3, HP = 13, Price = 15, Strength = 5, Tag = "Boots1", Part = BodyParts.Foot});
		elements.Add (new Element{ Path = "Icons/Boots2", Armor = 6, HP = 19, Price = 30, Strength = 11, Tag = "Boots2", Part = BodyParts.Foot});

		elements.Add (new Element{ Path = "Icons/Pants1", Armor = 15, HP = 40, Price = 89, Strength = 11, Tag = "Pants1", Part = BodyParts.Leg});
		elements.Add (new Element{ Path = "Icons/Pants2", Armor = 2, HP = 32, Price = 50, Strength = 23, Tag = "Pants2", Part = BodyParts.Leg});
	}

	/// Closes the app
	void Quit()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Application.Quit ();
		}
	}


	void OnMouseDown()
	{
		print ("Mouse down");
	}

}