using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]


public class Item : ScriptableObject
{

	[Header("Базовые характеристики")]
	public string Name = " ";
	public string EngName = " ";
	public string Description = "Описание предмета";
	public string EngDescription = "Item Description";
	public Sprite icon = null;

	[Header("Игровые характеристики")]
	public int time;


	[Header("Характеристики для торговли")]
	public int Coins;
	public int Gems;

	public string PlayerPrefsName;

}
