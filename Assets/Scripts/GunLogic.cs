using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private int _bulletsCount;
	[SerializeField] private float _delay = 0.2f;
	[SerializeField] private float _speed = 10f;

	private Queue<GameObject> _gunQueue;
	private GameObject _bullet;
	private float _time = 0f;

	#region Singleton class: GunLogic
	
	public static GunLogic Instance;

	void Awake()
	{
		Instance = this;
	}

	#endregion
	
	void Start()
	{
		PrepareBullets();
	}

	private void Update()
	{
		_time += Time.deltaTime;
		if (_time >= _delay)
		{
			_time = 0f;
			_bullet = SpawnBullet(transform.position);
			if (_bullet != null) _bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * _speed;
		}
	}

	private void PrepareBullets()
	{
		_gunQueue = new Queue<GameObject>();

		for (int i = 0; i < _bulletsCount; i++)
		{
			_bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, transform);
			_bullet.SetActive(false);
			_gunQueue.Enqueue(_bullet);
		}
	}

	public GameObject SpawnBullet(Vector2 position)
	{
		if (_gunQueue.Count > 0)
		{
			_bullet = _gunQueue.Dequeue();
			_bullet.transform.position = position;
			_bullet.SetActive(true);
			return _bullet;
		}

		return null;
	}
	public void DestroyBullet(GameObject bullet)
	{
		_gunQueue.Enqueue(bullet);
		bullet.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals("Bullet"))
		{
			DestroyBullet(other.gameObject);
		}
	}
}