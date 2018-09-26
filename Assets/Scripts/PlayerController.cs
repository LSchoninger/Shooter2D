using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed,movement;

	public bool facingRightSide;
	public GameObject around;
	private GameObject gun;
	private Vector3 positionMouse;
	
	void Start ()
	{

		positionMouse = new Vector3(0,0,0);
		gun = GameObject.FindGameObjectWithTag("Gun");
		facingRightSide = true;
	}

	void Update () {
		Movement();
		GunLookingAt();
	}
	void Movement()
	{
		movement = Input.GetAxis("Horizontal");
		transform.position += new Vector3(movement, 0, 0) * moveSpeed;
		if (movement < 0&&facingRightSide)
		{
			Flip();
		}

		if (movement > 0&&!facingRightSide)
		{
			Flip();
		}
	}

	void GunLookingAt()
	{
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
		gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation,rotation,5f*Time.deltaTime);

		/*positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		positionMouse = new Vector3(positionMouse.x,positionMouse.y,0);
		if (facingRightSide)
		{
			gun.transform.right = positionMouse - gun.transform.position;
			gun.transform.RotateAround(around.transform.position, new Vector3(0, 0, 1),1f);
				
		}

		if (!facingRightSide)
		{
			gun.transform.right = (positionMouse - gun.transform.position) * -1;
		}*/
	}

	void Flip()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Sprite");
		Vector3 objScale = objs[0].transform.localScale;
		objScale.x *= -1;
		objs[0].transform.localScale = objScale;
		facingRightSide =! facingRightSide;
	}
	
}
