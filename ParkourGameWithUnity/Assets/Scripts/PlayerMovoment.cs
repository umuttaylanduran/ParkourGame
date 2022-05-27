using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovoment : MonoBehaviour
{
    //kARAKTER HAREKET KODU
    private CharacterController controller; //bunu tan�mlamam�z�n sebebi playera verdi�imiz charakter contoller�n rigid body gibi i�inde �zelliklerinin bulunmas�.
    public float speed = 1f;

    //Kamera Kontroller
    private float xRotation = 0f; //bunu yapmam�z�n sebebi karakterin yukar� a�a�� bakarken bir s�n�rlamas�n�n olams� 90,-90 olmas� laz�m bu karakterin bak�� a��s�n�n s�n�rlamas�n� yapmak i�in de 
    // ilk �nce de�eri �nce elimizde tutup kontrol etmek istiyoruz.
    public float mouseSensivity = 100f;


    //JUMP AND GRAVITY
    private Vector3 velocity; //h�z i�in
    private float gravity = -9.81f; //yer �ekimi i�in static bir de�er olu�turduk. d�nya da da yer �ekimi bu de�erde 9.81. - olmas�n�n sebei a�a�� do�ru gitmesini istedi�imiz i�in.
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
        Cursor.visible = false; // mouse mizin imlecinin g�z�kmemesini sa�l�yor.
        Cursor.lockState = CursorLockMode.Locked; // mouse imlecini ne kadar hareket ettirsekte hep ortaya sabitleyecek.
    }

    private void Update()
    {

        //CHeck Character isGrounded
        isGround = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, obstacleLayer);


        //MOVEMENT //KARAKTER HARAKET ETME KODU SA� SOL �LER� GER� !!
        Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        Vector3 moveVelocity = moveInputs * Time.deltaTime * speed;

        controller.Move(moveVelocity);  //i�ine girilen vect�rle karakterin hareket etmesin isa�l�yor. component olarak ekledi�imiz character controllerin �zelli�ii.

        //Kamera Kontroller

        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity, 0);

        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //karakter tepe taklak olmas�n diye bu de�eri clapmle sabitliyoruz.max ve min.ilk de�er hangi de�er olaca��n� g�sterir ikinci min ���nc� max.

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // a�� e�itlemelerinde quaterninon euler kullan��l�r i�ine x rotation gerisi s�f�r kullan�ld�.

        //Jump and Gravity
        if (!isGround)
        {
            velocity.y += gravity * Time.deltaTime / gravityDivide;
            aTimer += Time.deltaTime / 3;
            speed = Mathf.Lerp(10, JumpSpeed, aTimer); //bu kod a de�erinden b noktas�na ne kadar s�rede gidece�ini hesaplar. timera ka� yazarsak oransal olarak girdi�imiz o iki say� aras�ndaki de�eri verecektir bize.       
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity / gravityDivide * Time.deltaTime); //form�l ��yle k�k i�inde ne kadar y�kseklik istersen �arp� -2 �arp� yer�ekimi.
        }
        controller.Move(velocity);





    }
}
