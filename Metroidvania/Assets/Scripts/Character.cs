﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Variaveis Públicas
    public float velocidadePadrao;   //Velocidade padrão do personagem
    public Transform groundCheck;   //Objeto vazio que servirá para verificar quando o personagem está no chão
    public float jumpForce;   //Força do pulo
    public float fireRate; //Velocidade de ataque do personagem


    //Variaveis Privadas 
    private Rigidbody2D rb;   //Variavel do tipo RigidBody para receber o RigidBody do personagem
    private float velocidade;   //Velocidade atual do personagem
    private bool facingRight = true;   //Variavel que armazena o lado que o personagem esta virado, sempre será iniciado para a direita.
    private bool onGround; //Variavel que detecta se o personagem está no chão
    private bool jump = false; //Variavel que vai verificar se o personagem já pulou para habilitar o doubleJump
    private bool doubleJump; //Variavel que controla para q só seja feito 1 pulo no ar
    private Weapons weaponEquipped; // Variavel para armazenar a arma que está equipada no jogador
    private Animator anim; // Variavel anim para gerenciar variaveis de trigger nas  animações
    private Attack attack; //Variavel que recebe paremetros da classe Attack para gerenciar itens e dano
    private float nextAttack; //Variavel que gerencia o momento do proximo ataque
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Variavel anim recebe o componente Animator para que possa ser gerenciado suas variaveis
        rb = GetComponent<Rigidbody2D>();  //Inserindo Rigidbody2D para gerencia atraves da variavel rb
        velocidade = velocidadePadrao; //Velocidade recebe a velocidade padrão do personagem
        attack = GetComponentInChildren<Attack>(); //Variavel attack recebe o componente do tipo Attack em um de seus objetos filhos (Children)
    }


    // Update is called once per frame
    void Update()
    {
        //Variavel que vai receber a informação de quando o player está no chão atraves de uma "linha" que é disparada do personagem ao objeto vazio groundCheck, quando encontrar a layer Ground vai acusar true
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //Quando está no chão doublejump desabilitado
        if (onGround)
                    doubleJump = false;
        //Verifica quando é pressionado o botão de pulo, se o personagem está no chão ele pula, se está no ar ele pula 1 vez
        if (Input.GetButtonDown("Jump") && (onGround || !doubleJump))
        {
            jump = true;
            if (!doubleJump && !onGround)
                doubleJump = true;
        }
    }

    //FixedUpdate para manter a taxa de quadros a mesma independente do poder de procesamento do computador, ideal para física do jogo
    private void FixedUpdate()
    {
         
        float h = Input.GetAxisRaw("Horizontal");  //Identifica quando está pressionado o botão de andar
        
        rb.velocity = new Vector2(h * velocidade, rb.velocity.y);  //aplica a movimentação no rb do personagem

        if( h > 0 && !facingRight) //Quando h for maior que zero e estiver virado para esquerda, botão D apertado logo virar para direita
        {
            Flip();
        }
        else if (h < 0 && facingRight) //Quando h for menor que zero e estiver virado para direita, botão A apertado logo virar para esquerda
        {
            Flip();
        }

        //Sempre que a variavel jump for true aplicará força para que o personagem suba
        if (jump)
        {
            rb.velocity = Vector2.zero;//zera a velocidade do player
            rb.AddForce(Vector2.up * jumpForce);//aplica força ao pulo
            jump = false;
        }

        //Quando for pressionado o botão de ataque e o tempo de ataque corresponder ao estipulado
        if (Input.GetButtonDown("Fire1") && Time.time > nextAttack && weaponEquipped != null )
        {
            anim.SetTrigger("Attack"); //Ativa o trigger da animação
            attack.PlayAnimation(weaponEquipped.animation); //Realiza a animação da arma equipada
            nextAttack = Time.time + fireRate; // Aumenta o valor de next attack para que o ataque seja disparado conforme o fireRate
        }

    }

    //Função para fazer o sprite do personagem virar
    void Flip()
    {
       
        facingRight = !facingRight;  //Inverte o estado do personagem
        
        Vector3 scale = transform.localScale;   //Variavel scale recebe a posição atual
        
        scale.x *= -1;   //variavel scale multiplicada por -1 para inverter o lado do personagem
        
        transform.localScale = scale;   //posição do personagem recebe valor de scale

    }

    //Função que realiza a alteração da arma equipada pelo jogador
    public void AddWeapon(Weapons weapon)
    {
        //Arma equipada recebe a arma que foi recebida pela função
        weaponEquipped = weapon;
        attack.setWeaponDamage(weaponEquipped.damage); // Damage estabelecido de acordo com a arma que o jogador utiliza
    }

}
