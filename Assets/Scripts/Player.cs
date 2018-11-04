using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float fireRate = 0.25f;
	[SerializeField]
	private float speed = 5.0f;
	[SerializeField]
	private GameObject laserPrefab;
	[SerializeField]
	private GameObject tripleShot;
	[SerializeField]
	private GameObject explosion;


	private bool canTripleShot = false;
	private bool isShieldActive = false;

	private float nextFire = 0.0f;
	private int lives = 1;

	
	// Use this for initialization
	void Start ()
	{
		transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		Movement();

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		if (Time.time > nextFire)
		{
			if (canTripleShot)
			{
				Instantiate(tripleShot, transform.position, Quaternion.identity);
			}
			else
			{
				Instantiate(laserPrefab, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
			}
			
			nextFire = Time.time + fireRate;
		}
	}
	private void Movement()
	{
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");

		transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime);

		if (transform.position.y > 0)
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		} 
		else if (transform.position.y < -3.0f)
		{
			transform.position = new Vector3(transform.position.x, -3.0f, 0);	
		}
		
		// constraint on the x axis

//		if (transform.position.x > 8.0f)
//		{
//			transform.position = new Vector3(8.0f, transform.position.y, 0);
//		} else if (transform.position.x < -8.0f)
//		{
//			transform.position = new Vector3(-8.0f, transform.position.y, 0);
//		}

		// wrap around x axis
		
		if (transform.position.x > 9.5f)
		{
			transform.position = new Vector3(-9.5f, transform.position.y, 0);
		}
		else if (transform.position.x < -9.5f)
		{
			transform.position = new Vector3(9.5f, transform.position.y, 0);
		}	
	}

	public void TripleShotPowerUp()
	{
		canTripleShot = true;
		StartCoroutine(TripleShotPowerDown());
	}
	private IEnumerator TripleShotPowerDown()
	{
		yield return new WaitForSeconds(5.0f);
		canTripleShot = false;
	}

	public void SpeedBoostPowerUp()
	{
		speed = speed * 1.5f;
		StartCoroutine(SpeedBoostPowerDown());
	}

	private IEnumerator SpeedBoostPowerDown()
	{
		yield return new WaitForSeconds(5.0f);
		speed = speed / 1.5f;
	}

    public void ShieldPowerUp()
    {
    	isShieldActive = true;
    }

	public void Hit()
	{
		if(isShieldActive)
		{
			isShieldActive = false;
		} else 
		{
			lives--;
		}
		
		if (lives == 0)
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
