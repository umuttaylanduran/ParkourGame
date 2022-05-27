using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    //Dronun bizi takip etmesi için yazýlan kod.

    private Transform player;
    public float follow_distance = 10f;

    private float cooldown = 2f;

    public GameObject mesh;
    public GameObject bullet;
    //public Vector3 offset;

    public float speed = 1f;
    public float health = 100f;

    public GameObject death_effect;
    public AudioClip death_sound;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FallowPlayer();
        Shot();
        Death();
    }

    private void FallowPlayer()
    {
        //Look to player
        transform.LookAt(player.position);
        transform.rotation *= Quaternion.Euler(new Vector3(-90, 0, 0));

        //Move To Player
        if (Vector3.Distance(transform.position, player.position) >= follow_distance) //a ile b noktasýndaki uzaklýðý veriyor bize ortalama
        {
            transform.Translate(transform.forward * Time.deltaTime * speed * -1);
        }
        else
        {
            //merkez ve dönme açýsý veriyorsun o bu eksen etrafýnda dönmeye baþlýyor
            //bizi takip etmediði zaman etrafýmýzda dönsün diye bunu yazýyoruz.
            transform.RotateAround(player.position, transform.forward, Time.deltaTime * speed * Random.Range(0.2f, 3f));
        }

    }

    private void Shot()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 2f;
            //Shot
            mesh.GetComponent<Animator>().SetTrigger("shot");
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }

    private void Death()
    {
        if (health <=0)
        {
            //Spawn particle

            Instantiate(death_effect, transform.position, Quaternion.identity);

            //Play Soundeffect
           GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(death_sound,0.4f);

            //Destroy gameobject
            Destroy(this.gameObject);


        }
    }
}

