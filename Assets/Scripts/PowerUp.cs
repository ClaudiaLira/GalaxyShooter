using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField]
	private float speed = 3.0f;

	[SerializeField] 
	private int powerupID; // 0 = tripleShot, 1 = speed, 2 = shield
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				switch (powerupID)
				{
					case 0:
					{
						// triple shot
						player.TripleShotPowerUp();
					}break;
					case 1:
					{
						//speed
						player.SpeedBoostPowerUp();
					}break;
					case 2:
					{
						//shield
						player.ShieldPowerUp();
					}break;
				}
				
				Destroy(this.gameObject);	
			}
		}
	}
}
