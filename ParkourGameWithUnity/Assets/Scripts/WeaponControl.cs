using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject hand; //bunu oluþturmamýz ve kullanmamýzdaki neden scripts ile hand i döndürürken idle animasyonu da sadece tabancaya eklemek böylelikle idle durma animasyonu ile
    // baktýðýmýz yere silahýn döndürülmesi karýþmayacak.

    public LayerMask obstacleLayer;
    public Vector3 offset;

    RaycastHit hit;

    //Fire
    public GameObject bullet;
    public Transform firePoint;

    private float coolDown;

    public AudioClip gunshot;

    private void Update()
    {       //LOOK

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, obstacleLayer))
        {
            // hit.point //bu oluþturdupumuz lazerimizin çarptýðý noktayý temsil ediyor.  
            //hit.transform.position = hit.transform.position deseydik bu lazerin çarptýðý objenin merkezini temsil ediyor olucaktý.
            //transform.LookAt(); bu ise hangi objeye atarsak bu scripti nereye attýysak o objenin bizim verdiðimiz deðer yani açýsýný otomatik olarak ona dönecek þekilde bakýþ açýsý oluþturur.

            hand.transform.LookAt(hit.point);
            hand.transform.rotation *= Quaternion.Euler(offset); //quaternion yapýlarý çarparak toplanýr. bunu yapmamýzýn sebebi silah ters duruyordu bunu yaparak ayrýetten bir açý ekledik silaha
            // mesela x ekseninde þu kadar derece dönsün gibi.

        }


        //CoolDown
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }

        //Shot
        if (Input.GetMouseButtonDown(0) && coolDown <= 0)
        {
            //Creat Bullet
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 270, 0));

            //Reset CoolDown
            coolDown = 0.25f;

            //Sound
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(gunshot, 0.4f);

            //Animation
            GetComponent<Animator>().SetTrigger("shot");
        }



    }
}
