using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller; // Chama o Controlador do Personagem da propria Unity
    private Animator animator;

    [Header("Player Config")]
    public float movementSpeed = 3f; // movimentação do personagem
    private Vector3 direction;
    private bool isWalk; // objeto boleano para definir se ele esta andando

    // inputs objetos
    private float horizontal;
    private float vertical;

    [Header("Attack Config")]
    public ParticleSystem fxAttack; // variavel para utilização de particulas do player
    public Transform hitBox; // variavel para 
    [Range(0.2f, 1f)]
    public float hitRange = 0.5f;
    public LayerMask hitMask;
    private bool isAttack; // variavel para dizer se o personagem esta atacando
    public Collider[] hitInfo;
    public int amountDamege;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // objeto para buscar o component da plataforma unity
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        MoveCharacter();
        UpdateAnimator();
        

    }

    #region MEUS METODOS

    // Metodo Responsavel pelos Controles do Player
    void Inputs()
    {
        horizontal = Input.GetAxis("Horizontal"); //Função do Unity que seta a direção e os controles dessa direção
        vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1") && isAttack == false) //Getbuttondown quando apertar tal botão
        { 
            AttackLeftClick(); 
        }
        else if (Input.GetButtonDown("Fire2"))
        { 
            AttackRigthClick(); 
        }
    }

    void AttackLeftClick()
    {
        isAttack = true;
        animator.SetTrigger("Attack02"); // ao clicar no button Fire1 vai acontecer essa trigger
        fxAttack.Emit(1);

        hitInfo = Physics.OverlapSphere(hitBox.position, hitRange, hitMask);

        foreach (Collider c in hitInfo)
        {
            c.gameObject.SendMessage("Gethit", amountDamege, SendMessageOptions.DontRequireReceiver);
        }
    }

    void AttackRigthClick()
    {
        animator.SetTrigger("Attack01");
    }

    void AttackIsDone()
    {
        isAttack = false;
    }

    // Metodo responsavel pela movimentação do player
    void MoveCharacter()
    {
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
    }

    // Metodo responsavel pela animação do player
    void UpdateAnimator()
    {
        animator.SetBool("isWalk", isWalk);
    }

    #endregion  

    private void OnDrawGizmosSelected() 
    {
        if(hitBox != null) { 
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitBox.position, hitRange);
        }
    }

}
