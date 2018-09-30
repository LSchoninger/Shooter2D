using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed,movement;

	public bool facingRightSide;
	private GameObject gun;
	private Vector3 positionMouse;
	private bool isJumping;
	public GameObject gunPoint;
	
	void Start ()
	{

		positionMouse = new Vector3(0,0,0);
		gun = GameObject.FindGameObjectWithTag("Gun");
		facingRightSide = true;
	}

	void Update () {
		Movement();
		GunLookingAt();
		Shoot();
	}


	void Shoot()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hit;
			positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			positionMouse.z = 0;
			hit = Physics2D.Raycast(gunPoint.transform.position,positionMouse ,Mathf.Infinity);
			Debug.DrawRay(gunPoint.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
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
		//Pulo
		if(Input.GetKeyDown(KeyCode.Space)&&isJumping==false)
		{
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 9), ForceMode2D.Impulse);
			isJumping = true;
		}
	}

	void GunLookingAt()
	{
		//controla onde a arma esta olhando;
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
		gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation,rotation,5f*Time.deltaTime);
	}

	void Flip()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Sprite");
		Vector3 objScale = objs[0].transform.localScale;
		objScale.x *= -1;
		objs[0].transform.localScale = objScale;
		facingRightSide =! facingRightSide;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground")
		{
			//Quando bater no chao, boleana para nao pular de novo reset
			isJumping = false;
		}
	}
}
