using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    //Enemy Movement Variables
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public Transform target;


    //Spawn Enemy Variables
    public GameObject enemies;
    public int xPos;
    public int zPos;
    public int spawnedEnemies;

    public Flowchart ConvowBird;
    public Flowchart ending;

    //shooting
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform shooter;

    //Enemy Stats
    public float health = 10f;

    public bool end;
    public bool hit = false;

    public ParticleSystem gotHit;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ConvowBird.GetBooleanVariable("ConvowBird") == true)
        {
            StartCoroutine(Resume());
        }

        if (health <= 0f)
        {
            Death();
        }

        if (hit == true)
        {
            gotHit.Play();
        }

        Movement();

    }

    void Movement() //Enemy will look at player or whatever GameObject you attach to the target in the boss script
    {
        if(target != null)
        {
            transform.LookAt(target);
        }
    }

    void shotDelay() //Set the amount of time in between shots at the player
    {
        if (end == false)
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, shooter.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public void Damaged(float amount)
    {
        Debug.Log("Boss Health " + health);
        health -= amount;
        justHit();
        Invoke("notHit", 1f);
    }

    void justHit()
    {
        hit = true;
    }

    void notHit()
    {
        hit = false;
    }

    void Death()
    {
        SceneManager.LoadScene("GoodEnding"); 
    }

    IEnumerator Resume()
    {
        yield return new WaitForSeconds(10f);
        shotDelay();
       
    }

}
