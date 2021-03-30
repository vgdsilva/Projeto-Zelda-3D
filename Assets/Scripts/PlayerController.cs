using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller; // Chama o Controlador do Personagem da propria Unity
    private Animator animator;

    [Header("Config Player")]
    public float movementSpeed = 3f; // movimentação do personagem

    private Vector3 direction;
    private bool isWalk; // objeto boleano para definir se ele esta andando


    [Header("Camera")]
    public GameObject camB; // objeto de camera para controlar outros tipos de camera
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // objeto para buscar o component da plataforma unity
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Função do Unity que seta a direção e os controles dessa direção
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1")) //Getbuttondown quando apertar tal botão
        {
            animator.SetTrigger("Attack02"); // ao clicar no button Fire1 vai acontecer essa trigger

        }else if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Attack01");
        }



        direction = new Vector3(horizontal, 0f, vertical).normalized; // Função para movimentar o personagem nas respectivas direções

        if (direction.magnitude > 0.1f) // Função para calcular a Rotação do Player
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            isWalk = true;
        }
        else // if (direction.magnitude <= 0.1f)
        {
            isWalk = false;
        }

        controller.Move(direction * movementSpeed * Time.deltaTime); // Função ja existente dentro da unity para controlar o personagem
        animator.SetBool("isWalk", isWalk);

    }

    //Sistema de Camera dinamica

    private void OnTriggerEnter (Collider other)
    {
        switch (other.gameObject.tag)  // vai mudar quando colidir com o CamTrigger que é o objeto criado 
        {
            case "CamTrigger":
                camB.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:
                camB.SetActive(false);
                break;
        }
    }

    // Fim sistema de Camera Dinamida
}
