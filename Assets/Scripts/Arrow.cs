using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float Force;

	private Rigidbody myRigidbody;
	private bool isFlying;

	void Awake() {
		myRigidbody = GetComponent<Rigidbody> ();
	}

	void Start() {
		isFlying = true;
		myRigidbody.AddForce (new Vector3(transform.forward.x * Force, transform.forward.y * Force, transform.forward.z * Force));
	}

	void Update() {
		if (isFlying) {
			transform.rotation = Quaternion.LookRotation (myRigidbody.velocity);
		}
		//Debug.Log (transform.rotation);
	}

	void OnTriggerEnter(Collider other) {
		//if (other.GetComponent<Arrow>() == null) {
			isFlying = false;
			myRigidbody.velocity = Vector3.zero;
			Destroy (myRigidbody);
			//myRigidbody.useGravity = false;
		//}
	}
}
