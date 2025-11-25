using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] private float speed;
	[SerializeField] private List<GameObject> trails;
	[SerializeField] private GameObject hitPrefab;
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
        //transform.position += transform.forward * speed * Time.deltaTime;

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

			if (trails.Count > 0) 
            {
				for (int i = 0; i < trails.Count; i++) 
                {
					trails[i].transform.parent = null;
					ParticleSystem ps = trails[i].GetComponent<ParticleSystem>();
					if (ps != null) {
						ps.Stop();
						Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
					}
				}
			}

            ContactPoint contact = collision.GetContact(0);
			Quaternion rot = Quaternion.FromToRotation (Vector3.up, contact.normal);
			Vector3 pos = contact.point;


            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);

				var ps = hitVFX.GetComponent<ParticleSystem> ();
				if (ps == null) {
					var psChild = hitVFX.transform.GetChild (0).GetComponent<ParticleSystem> ();
					Destroy (hitVFX, psChild.main.duration);
				} else
					Destroy (hitVFX, ps.main.duration);
            }
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
