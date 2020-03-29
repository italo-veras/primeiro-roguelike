using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMago : BaseInimigo
{   
    private float tempoQueSeMove;
    public float quandoSeMove = 3; //se move a cada 3 segundos 

    public float velocidadeDeAtaque, quantoVaiMover;
    private float contadorParaAtirar;

    public Transform porOndeVaiSairOTiro;
    public GameObject projectil;
    int direcao;

    
    private void Awake()
    {
        direcao = Random.Range(1, 3);
    }
    protected override void Mover()
    {
        // base.Mover();
        MoverAleatoriamente();
        
    }

    private void MoverAleatoriamente()
    {
        tempoQueSeMove += Time.deltaTime;
        if(tempoQueSeMove > quandoSeMove && Vector3.Distance(transform.position, alvo.position)< distanciaParaVerOAlvo)
        {
            if(direcao == 1)
            {
                transform.position = new Vector3(transform.position.x + quantoVaiMover, transform.position.y, transform.position.z);
                transform.LookAt(alvo.position);

                direcao = Random.Range(3,4);
            }else if (direcao == 2)
            {
                transform.position = new Vector3(transform.position.x - quantoVaiMover, transform.position.y, transform.position.z);
                transform.LookAt(alvo.position);

                direcao = Random.Range(3,4);

            } else if (direcao == 3)
            {
                transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z + quantoVaiMover);
                transform.LookAt(alvo.position);

                direcao = Random.Range(1, 3);
            } else if (direcao == 4)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - quantoVaiMover);
                transform.LookAt(alvo.position);

                direcao = Random.Range(2, 3);
            }


            tempoQueSeMove = 0;
        }
            
    }
    protected override void Atacar()
    {
        base.Atacar();

        contadorParaAtirar += Time.deltaTime;
        if (Vector3.Distance(transform.position, alvo.position) < distanciaParaVerOAlvo)
        {
            
            if (contadorParaAtirar > velocidadeDeAtaque)
            {
                Instantiate(projectil, porOndeVaiSairOTiro.position, Quaternion.identity);
                contadorParaAtirar = 0;
            }
        }
            
    }
}
