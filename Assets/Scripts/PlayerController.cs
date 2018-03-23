using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 15.0f;
	public float padding = 2f;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float firingRate = 0.2f;
	public float health = 250;
	public AudioClip projectileSound;

	float xMin;
	float xMax;

	private HealthKeeper healthKeeper;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
		healthKeeper = GameObject.FindObjectOfType<HealthKeeper>();
	}

	void FireProjectile() {
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
		Vector3 soundPos = transform.position + new Vector3(0f, 0f, -9.5f);
		AudioSource.PlayClipAtPoint(projectileSound, soundPos, 1f);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile) {
			health -= missile.GetDamage();
			healthKeeper.UpdateHealth(health);
			missile.Hit();
			if(health <= 0) {
				Die();
			}
		}
	}

	void Die() {
		LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		manager.LoadLevel("Win");
		Destroy(gameObject);
		HealthKeeper.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		//fire projectile
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("FireProjectile", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("FireProjectile");
		}

		//control player ship
		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * moveSpeed * Time.deltaTime;
		}

		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
