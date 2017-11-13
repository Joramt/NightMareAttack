using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MineManager : MonoBehaviour
{
	public static int nbMine;
	
	
	Text text;
	
	
	void Awake ()
	{
		text = GetComponent <Text> ();
		nbMine = GameMaster.currentMines;
	}
	
	
	void Update ()
	{
		text.text = "Mines : " + nbMine + " / 3 ";
	}
}