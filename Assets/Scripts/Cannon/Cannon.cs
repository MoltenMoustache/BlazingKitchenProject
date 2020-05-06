using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject fireball;
    public bool stopspawning = false;
    public float spawnTime;
    public float spawnDelay;
    Timer timer = null;

    Animator animator;
    [SerializeField] AnimationClip animation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("Shoot", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            timer.Tick(Time.deltaTime);
        }

    }
    public void SpawnObject()
    {
        Instantiate(fireball, spawnPos.position, spawnPos.rotation);
        if (stopspawning)
        {
            CancelInvoke("Shoot");
        }
        animator.SetBool("isShooting", false);

        timer = null;
    }

    public void Shoot()
    {
        if (timer == null)
        {
            timer = new Timer(animation.length - 0.5f, SpawnObject);
            animator.SetBool("isShooting", true);
        }
    }
}
