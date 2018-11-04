using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private GameObject explosion;
	[SerializeField] private float speed = 3.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime);

		if (transform.position.y < -6)
		{
			//respawn
			transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				player.Hit();
			}
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
		else if(other.tag == "Laser")
		{
			Laser laser = other.GetComponent<Laser>();
			if (laser != null)
			{
				laser.DestroyLaser();	
			}
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
		
	}
}
