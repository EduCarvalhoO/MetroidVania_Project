using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
//Script utilizado para armazenar informaçõs das armas, será herdado em todo item tipo espada que estiver no game. 
public class Weapons : ScriptableObject
{
   // Variaveis publicas 
   public int itemID; //Utilizado para referenciar o item futuramente, tanto nos saves como no inventário
    public string weaponName; //nome da arma
    public string description; //breve descrição da arma que irá aparecer 
    public int damage; //Dano que a arma causa nos inimigos
    public Sprite image; // Sprite da arma que ira aparecer no inventario e quando a arma dropar de um mob
    public AnimationClip animation; //Animação de ataque da espada
    public string message; //Mensagem que irá aparecer na tela ao obter o item

}
