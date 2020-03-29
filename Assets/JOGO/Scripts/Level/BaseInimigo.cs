using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInimigo : MonoBehaviour
{
    [SerializeField] protected private string nomeDoInimigo;
    [SerializeField] protected private float velocidadeDeMovimento;
    protected private float pontosDeVida;
    [SerializeField] protected private float pontosDeVidaMaximo;

    protected private Transform alvo;
    [SerializeField] protected private float distanciaParaVerOAlvo;

    protected private Rigidbody meuRigidbody;

    // Update is called once per frame
    private void Start()
    {
        pontosDeVida = pontosDeVidaMaximo;
        meuRigidbody = GetComponent<Rigidbody>();
        alvo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if(pontosDeVida <= 0)
        {
            Morrer();
        }
    }
    private void FixedUpdate()
    {
        Atacar();
        Mover();
    }
    protected virtual void Mover()
    {
        if(Vector3.Distance(transform.position,alvo.position) < distanciaParaVerOAlvo)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidadeDeMovimento * Time.deltaTime);
            transform.LookAt(alvo);
        }
    }
    protected virtual void Atacar()
    {
        
    }

    protected virtual void Morrer()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // mudar a tag para cada projetil, cada tag vai tirar uma vida especifica 

        if(other.tag == "Projetil")
        {
            pontosDeVida -= 10;
        }
        print(pontosDeVida + "Vida faltando");
    }

}
