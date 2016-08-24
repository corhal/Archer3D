using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float Force;

	private Rigidbody myRigidbody;
	private bool isFlying;
	Vector3 initialPosition;

	void Awake() {
		myRigidbody = GetComponent<Rigidbody> ();
	}

	void Start() {
		initialPosition = new Vector3 (transform.position.x, 0.0f, transform.position.z);
		isFlying = true;
		float initialZ = transform.position.z;
		Vector3 force = new Vector3(transform.forward.x * Force, transform.forward.y * Force, transform.forward.z * Force);
		myRigidbody.AddForce (force, ForceMode.VelocityChange);
		float angle = Vector3.Angle (transform.forward, Vector3.forward);
		float vel0sq = Mathf.Pow (force.magnitude, 2.0f);
		float radAngle = Mathf.Deg2Rad * angle;
		float h = transform.position.y;
		//Debug.Log (h);
			
		float secondPart = 1 + Mathf.Sqrt (1 + (2 * Physics.gravity.magnitude * h) / (vel0sq * Mathf.Sqrt(Mathf.Sin(radAngle))));
		//Debug.Log (secondPart.ToString ());
		//Debug.Log (Mathf.Sin (2 * radAngle));
		float distance = (vel0sq / (2 * Physics.gravity.magnitude)) * Mathf.Sin (2 * radAngle) * (secondPart);
		//Debug.Log (distance);
		//Debug.Log (distance + initialZ);
		//Debug.Log (transform.position.z + transform.position.x);

	}

	void Update() {
		if (isFlying) {
			transform.rotation = Quaternion.LookRotation (myRigidbody.velocity);
		}
		//Debug.Log (transform.rotation);
	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponentInParent<Arrow>() == null) {			
			isFlying = false;
			myRigidbody.velocity = Vector3.zero;
			transform.SetParent (other.transform);
			Destroy (GetComponentInChildren<Collider> ());
			Destroy (myRigidbody);
			//myRigidbody.useGravity = false;
		}
		Vector3 myPosition = new Vector3 (transform.position.x, 0.0f, transform.position.z);
		Debug.Log(Vector3.Distance(initialPosition, myPosition));
	}
}
