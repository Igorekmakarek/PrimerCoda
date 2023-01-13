using System.Collections; using System.Collections.Generic; using UnityEngine;  public class CameraMovement : MonoBehaviour {  	public GameObject Player; 	private Vector3 offset;  	private Player plr;     [HideInInspector] 	public Vector3 moveTo; 	bool canMove; 	  	void Start () { 		 		offset = transform.position - Player.transform.position; 		Player.transform.position = new Vector3(transform.position.x, transform.position.y, 0f); 		plr = Player.GetComponent<Player>(); 		canMove = true; 	}
    private void LateUpdate()
    {
        if (Player != null)
        {
           
            moveTo = new Vector3(Player.transform.position.x + offset.x + plr.mass / 3f, Player.transform.position.y + offset.y, transform.position.z);
            transform.position = moveTo;
        }
        else
            Debug.Log("Player is not set in CameraMovement!");
    }
    //void LateUpdate () {
    //	if (Player != null)
    //	{
    //		moveTo = new Vector3(Player.transform.position.x + offset.x + plr.mass / 3, Player.transform.position.y + offset.y, transform.position.z);
    //		LeanTween.move(gameObject, moveTo, 5f * Time.deltaTime);
    //		//if (canMove)
    //		//	StartCoroutine(moveCamera());
    //	}
    //	else
    //		Debug.Log("Player is not set in CameraMovement!");
    //}

    //IEnumerator moveCamera()
    //   {
    //	canMove = false;
    //	//yield return new WaitForSeconds(0.2f);
    //	canMove = true;
    //	LeanTween.move(gameObject, moveTo, 5f * Time.deltaTime);
    //}
}