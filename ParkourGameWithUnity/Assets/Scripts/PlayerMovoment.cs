using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovoment : MonoBehaviour
{
    //kARAKTER HAREKET KODU
    private CharacterController controller; //bunu tanýmlamamýzýn sebebi playera verdiðimiz charakter contollerýn rigid body gibi içinde özelliklerinin bulunmasý.
    public float speed = 1f;

    //Kamera Kontroller
    private float xRotation = 0f; //bunu yapmamýzýn sebebi karakterin yukarý aþaðý bakarken bir sýnýrlamasýnýn olamsý 90,-90 olmasý lazým bu karakterin bakýþ açýsýnýn sýnýrlamasýný yapmak için de 
    // ilk önce deðeri önce elimizde tutup kontrol etmek istiyoruz.
    public float mouseSensivity = 100f;


    //JUMP AND GRAVITY
    private Vector3 velocity; //hýz için
    private float gravity = -9.81f; //yer çekimi için static bir deðer oluþturduk. dünya da da yer çekimi bu deðerde 9.81. - olmasýnýn sebei aþaðý doðru gitmesini istediðimiz için.
    private bool isGround;

    public Transform groundChecker;
    public float groundCheckerRadius;
    public LayerMask obstacleLayer;

    public float jumpHeight = 0.2f;
    public float gravityDivide = 10f;
    public float JumpSpeed = 30f;

    private float aTimer;


    private void Awake()
    {    //kARAKTER HAREKET KODU
        controller = GetComponent<CharacterController>();

        //Cursor
        Cursor.visible = false; // mouse mizin imlecinin gözükmemesini saðlýyor.
        Cursor.lockState = CursorLockMode.Locked; // mouse imlecini ne kadar hareket ettirsekte hep ortaya sabitleyecek.
    }

    private void Update()
    {

        //CHeck Character isGrounded
        isGround = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, obstacleLayer);


        //MOVEMENT //KARAKTER HARAKET ETME KODU SAÐ SOL ÝLERÝ GERÝ !!
        Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        Vector3 moveVelocity = moveInputs * Time.deltaTime * speed;

        controller.Move(moveVelocity);  //içine girilen vectörle karakterin hareket etmesin isaðlýyor. component olarak eklediðimiz character controllerin özelliðii.

        //Kamera Kontroller

        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity, 0);

        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //karakter tepe taklak olmasýn diye bu deðeri clapmle sabitliyoruz.max ve min.ilk deðer hangi deðer olacaðýný gösterir ikinci min üçüncü max.

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // açý eþitlemelerinde quaterninon euler kullanýýlýr içine x rotation gerisi sýfýr kullanýldý.

        //Jump and Gravity
        if (!isGround)
        {
            velocity.y += gravity * Time.deltaTime / gravityDivide;
            aTimer += Time.deltaTime / 3;
            speed = Mathf.Lerp(10, JumpSpeed, aTimer); //bu kod a deðerinden b noktasýna ne kadar sürede gideceðini hesaplar. timera kaç yazarsak oransal olarak girdiðimiz o iki sayý arasýndaki deðeri verecektir bize.       
        }
     
        else
        {
            velocity.y = -0.05f;
            speed = 10;
            aTimer = 0;
        }

        //Jump with space
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity / gravityDivide * Time.deltaTime); //formül þöyle kök içinde ne kadar yükseklik istersen çarpý -2 çarpý yerçekimi.
        }
        controller.Move(velocity);





    }
}
