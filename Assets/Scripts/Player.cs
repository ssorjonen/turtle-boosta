using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Transform rocketLauncherPosition;
    public Transform rocketLauncher;
    public Transform rocketLauncherSpawnPosition;
    public GameObject rocket;
    public ParticleSystem muzzleFlash;

    public BoxCollider2D hitbox;


    [Header("Models")]
    public GameObject normalTurtle;
    public GameObject hidingTurtle; 
    public bool hiding = false;

    [Header("Impact Sounds")]
    public AudioClip[] low;
    public AudioClip[] mid;
    public AudioClip[] big;

    public AudioClip fireSound;
    public AudioClip squishSound;

    public float nextShot = 1;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRules.gameIsRunning)
        {
            rb.simulated = true;
            nextShot += Time.deltaTime;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (mousePosition - (Vector2)rocketLauncherSpawnPosition.position).normalized;

            rocketLauncher.right = direction;

            if (Input.GetMouseButton(0) && nextShot >= 0.8f)
            {
                Instantiate(rocket, rocketLauncherSpawnPosition.position, rocketLauncher.rotation);
                nextShot = 0;
                Instantiate(muzzleFlash, rocketLauncherSpawnPosition.transform.position, rocketLauncher.rotation);
                AudioTool.ShootAudio(fireSound, transform);
            }

            // turtle goes hidign
            if (rb.velocity.magnitude > 2)
            {
                if (!hiding)
                {
                    DisplayTurtle(false);
                }
            }
            else {
                if (hiding)
                {
                    DisplayTurtle(true);
                }
            }
        }

        else
        {
            rb.simulated = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        int random = 0;
        if (col.relativeVelocity.magnitude > 3)
        {
            if (col.relativeVelocity.magnitude > 6)
            {
                if (col.relativeVelocity.magnitude > 9)
                {
                    random = Random.Range(1, 2);
                    AudioTool.ShootAudio(big[random], transform);
                    return;
                }
                random = Random.Range(1, 3);
                AudioTool.ShootAudio(mid[random], transform);
                return;
            }
            random = Random.Range(1, 3);
            AudioTool.ShootAudio(low[random], transform);
            return;
        }
    }

    public void Bounce()
    {
        float angle = Vector2.Angle(rb.velocity, Vector2.down);

        if (Mathf.Abs(rb.velocity.y) < 1.5) return;
        if (angle < 45) return;

        rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y / 1.5f);


    }

    public void Die()
    {
        AudioTool.ShootAudio(squishSound, transform, Random.Range(0.9f, 1.1f));
        GameRules.current.StopGame();
    }

    public void DisplayTurtle(bool yes)
    {
        if (yes == false)
        {
            hiding = true;
            normalTurtle.SetActive(false);
            hidingTurtle.SetActive(true);

            Vector2 size = hitbox.size;
            Vector2 offset = hitbox.offset;

            size.y = 0.7857003f;
            offset.y = 0.1071498f;
            hitbox.size = size;
            hitbox.offset = offset;
        }

        if (yes == true)
        {
            hiding = false;
            normalTurtle.SetActive(true);
            hidingTurtle.SetActive(false);

            Vector2 size = hitbox.size;
            Vector2 offset = hitbox.offset;

            size.y = 1;
            offset.y = 0;
            hitbox.size = size;
            hitbox.offset = offset;
              
        }
    } 

}
