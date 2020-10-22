using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    //Variaveis publicas
    public Weapons weapon; //Variavel que irá armazenar as informações do item arma

    //Variaveis privadas
    private SpriteRenderer sprite; // Variavel que vai receber a imagem da arma armazenada pela variavel weapon
    // Start is called before the first frame update
    void Start()
    {

        sprite = GetComponent<SpriteRenderer>(); // variavel sprite recebe o componemte SpriteRenderer para poder armazenar a imagem
        sprite.sprite = weapon.image; // propriedade sprite da variavel sprite recebe a imagem armazenada na variavel image da arma

    }


    //Função que irá detectar a colisão do objeto 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Variavel do tipo Character criada para armazenar o componente do colisor (other) e pegar as informações da propriedade Character
        Character player = other.GetComponent<Character>();

        //Quando a variavel player estiver com informações do personagem armazenadas irá realizar a função
        if (player != null)
        {
            player.AddWeapon(weapon); //Adiciona a arma encontrada a variavel equippedWeapon dentro da classe Character
            Destroy(gameObject); // Destroi o game object após ser coletado
        }

    }
}
