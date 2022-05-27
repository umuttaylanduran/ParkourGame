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
            broke = true; //ayak basýnca broke true oluyor.
            
        }
        if (broke) //broke true olursa eðer yavaþ yavaþ aþaðý inmeye baþla z ekseni kullanmamýzýn sebebi translate de öyleymiþ aþaðý inmesi için
        {               //biz bir kere ayak basýcaz ve bit diðer column a gittiðimiz zaman da o aþaðý yukarý oynamaya devam edecek.
                        // eðer yukardaki if  in içine yazsaydýk aþaðý in diye biz sadece ayak basýnca inecekti biz gittiðimizde de hareket etmesini istiyoruz.
            velocity.z -= Time.deltaTime / 200;
            transform.Translate(velocity);
        }
    }

}
