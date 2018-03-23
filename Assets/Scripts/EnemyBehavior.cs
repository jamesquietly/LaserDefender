using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
	public float health = 150;
	public GameObject Projectile;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	public AudioClip projectileSound;
	public AudioClip deathSound;

	private float projectileSpeed = 5f;
	private ScoreKeeper scoreKeeper;
	private Vector3 closerSoundOffset = new Vector3(0f, 0f, -9.5f);

	void Start() {
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0) {
				Die();
			}
		}
	}

	void Die() {
		Destroy(gameObject);
		scoreKeeper.UpdateScore(scoreValue);
		AudioSource.PlayClipAtPoint(deathSound, transform.position + closerSoundOffset, 1f);
	}

	void FireProjectile() {
		GameObject beam = Instantiate(Projectile, transform.position, Quaternion.identity);
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -projectileSpeed, 0f);
		AudioSource.PlayClipAtPoint(projectileSound, transform.position + closerSoundOffset, 1f);
	}

	void Update() {
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability) {
			FireProjectile();
		}
	}
}
