using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    float speed = 15f;
    float radius = 2.5f;
    public ParticleSystem explosion;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRules.gameIsRunning)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player") return;
        if (col.gameObject.layer == 4) return;
        if (col.gameObject.layer == 8) return;
        Explode();
    }

    void Explode()
    {
        Vector3 pos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 2.5f);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            float distance = 1f; //- Vector2.Distance(rb.transform.position, pos) / 2.5f;

            Vector2 direction = (rb.transform.position - pos).normalized;
            rb.AddForce(direction * (distance * 500f));
        }

        Instantiate(explosion, transform.position, transform.rotation);
        float pitch = Random.Range(0.9f, 1.1f);
        AudioTool.ShootAudio(explosionSound, transform, pitch);
        Destroy(this.gameObject);
    }
}
