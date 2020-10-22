using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Animator anim; // Animação do ataque
    private int damage; // Variavel para armazenar o dano

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // PEga o componente Animator do objeto que possui o script
    }

    //Inicia a animação de ataque
    public void PlayAnimation(AnimationClip clip)
    {
        anim.Play(clip.name); 
    }

    //recebe o dano do item e armazena na variavel
    public void setWeaponDamage(int damageValue)
    {
        damage = damageValue;
    }
}
