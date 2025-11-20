using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] 
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        isCollided = false;
		rb = GetComponent<Rigidbody>();
        if(rb == null) { Debug.LogError("RigidBody is nullptr"); }
    }

    private void FixedUpdate()
    {
        rb.position += (transform.forward) * (speed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isCollided)
        {
            isCollided = true;
            rb.isKinematic = true;
            StartCoroutine(DestroyParticle(0f));
        }
    }

    private IEnumerator DestroyParticle (float waitTime) 
    {
		yield return new WaitForSeconds (waitTime);
		Destroy (gameObject);
	}

    private Rigidbody rb;
    private bool      isCollided;
}
