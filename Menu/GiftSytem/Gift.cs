using UnityEngine;

[CreateAssetMenu(fileName = "New Gift", menuName = "MakeGift/Gift")]


public class Gift : ScriptableObject
{
	[Header("Отображение")]
	public Sprite Icon;

	[Header("Ресурсы")]
	public int coins;
	public int gems;

	[Header("Количество усилений")]
	public int heart;
	public int protection;
	public int slower;
	public int magnet;
	public int accelerator;
	public int fishfood;

}

