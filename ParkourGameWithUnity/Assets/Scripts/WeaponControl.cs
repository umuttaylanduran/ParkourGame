using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject hand; //bunu olu�turmam�z ve kullanmam�zdaki neden scripts ile hand i d�nd�r�rken idle animasyonu da sadece tabancaya eklemek b�ylelikle idle durma animasyonu ile
    // bakt���m�z yere silah�n d�nd�r�lmesi kar��mayacak.

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
            // hit.point //bu olu�turdupumuz lazerimizin �arpt��� noktay� temsil ediyor.  
            //hit.transform.position = hit.transform.position deseydik bu lazerin �arpt��� objenin merkezini temsil ediyor olucakt�.
            //transform.LookAt(); bu ise hangi objeye atarsak bu scripti nereye att�ysak o objenin bizim verdi�imiz de�er yani a��s�n� otomatik olarak ona d�necek �ekilde bak�� a��s� olu�turur.

            hand.transform.LookAt(hit.point);
            hand.transform.rotation *= Quaternion.Euler(offset); //quaternion yap�lar� �arparak toplan�r. bunu yapmam�z�n sebebi silah ters duruyordu bunu yaparak ayr�etten bir a�� ekledik silaha
            // mesela x ekseninde �u kadar derece d�ns�n gibi.

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
