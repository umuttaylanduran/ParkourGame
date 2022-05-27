using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{

    public Transform GroundCheckerColumn;
    public LayerMask player_layer;

    public float radius;

    private Vector3 velocity;

    private bool broke = false;

    private void Update()
    {
        if (Physics.CheckBox(GroundCheckerColumn.position, new Vector3(radius,2,radius), Quaternion.identity,player_layer))
        {
            broke = true; //ayak bas�nca broke true oluyor.
            
        }
        if (broke) //broke true olursa e�er yava� yava� a�a�� inmeye ba�la z ekseni kullanmam�z�n sebebi translate de �yleymi� a�a�� inmesi i�in
        {               //biz bir kere ayak bas�caz ve bit di�er column a gitti�imiz zaman da o a�a�� yukar� oynamaya devam edecek.
                        // e�er yukardaki if  in i�ine yazsayd�k a�a�� in diye biz sadece ayak bas�nca inecekti biz gitti�imizde de hareket etmesini istiyoruz.
            velocity.z -= Time.deltaTime / 200;
            transform.Translate(velocity);
        }
    }

}
