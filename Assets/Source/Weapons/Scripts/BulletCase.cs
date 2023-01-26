using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletCase : MonoBehaviour 
{
	[SerializeField] private float _minimumXForce;		
	[SerializeField] private float _maximumXForce;
	[SerializeField] private float _minimumYForce;
	[SerializeField] private float _maximumYForce;
	[SerializeField] private float _minimumZForce;
	[SerializeField] private float _maximumZForce;
	[SerializeField] private float _minimumRotation;
	[SerializeField] private float _maximumRotation;
	[SerializeField] private float _despawnTime;
	[SerializeField] private float _speed = 2500.0f;

	private Rigidbody _rigidbody;
	private AudioSource[] _sounds;
	private Transform _point;
	private bool _isGameStarted;

	private void Awake () 
	{
		_sounds = GetComponentsInChildren<AudioSource>();
		_rigidbody = GetComponent<Rigidbody>();
	}

    private void Start()
    {
        _isGameStarted = true;
    }

    private void FixedUpdate() 
	{
		transform.Rotate(Vector3.right, _speed * Time.deltaTime);
		transform.Rotate(Vector3.down, _speed * Time.deltaTime);
	}

	private void OnEnable()
	{
		AddForce();
		AddTorque();
		StartCoroutine(RemoveCasing());
		transform.rotation = Random.rotation;

		if(_isGameStarted)
			StartCoroutine(PlaySound());
	}

    private void OnDisable()
    {
		_rigidbody.velocity = Vector3.zero;
	}

    public void SetValuePositionRotation(Transform point)
    {
		_point = point;
		transform.position = _point.transform.position;
		transform.rotation = _point.transform.rotation;
	}

	private void AddForce()
    {
		_rigidbody.AddRelativeForce(
			Random.Range(_minimumXForce, _maximumXForce),
			Random.Range(_minimumYForce, _maximumYForce),
			Random.Range(_minimumZForce, _maximumZForce));
	}

	private void AddTorque()
    {
		_rigidbody.AddRelativeTorque(Random.Range(
			_minimumRotation, _maximumRotation),
			Random.Range(_minimumRotation, _maximumRotation),
			Random.Range(_minimumRotation, _maximumRotation)
			* Time.deltaTime);
	}

	private IEnumerator PlaySound() 
	{
		yield return new WaitForSeconds (Random.Range(0.25f, 0.85f));

		_sounds[Random.Range(0, _sounds.Length)].Play();
    }

	private IEnumerator RemoveCasing() 
	{
		yield return new WaitForSeconds (_despawnTime);

		gameObject.SetActive(false);
	}
}