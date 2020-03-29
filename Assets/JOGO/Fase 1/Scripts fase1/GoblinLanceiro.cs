using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinLanceiro : BaseInimigo
{

    [SerializeField] private float velocidadeDeAtaque, distanciaParaParar, distanciaParaRecuar;
    private float contadorParaAtirar;

    public GameObject projectil;
    bool vendo;
    public Transform porOndeVaiSairOTiro;

    private void Awake()
    {

    }
    protected override void Mover()
    {
        //base.Mover();
        MoverQuandoNaoVer();
    }
    protected override void Atacar()
    {
        base.Atacar();

        contadorParaAtirar += Time.deltaTime;

        if ((contadorParaAtirar > velocidadeDeAtaque) && (Vector3.Distance(transform.position, alvo.position) < distanciaParaVerOAlvo))
        {
            Instantiate(projectil, porOndeVaiSairOTiro.position, Quaternion.identity);
            contadorParaAtirar = 0;
        }
    }
    private void MoverQuandoNaoVer()
    {

      
        if(Vector3.Distance(transform.position, alvo.position) >  distanciaParaParar && Vector3.Distance(transform.position, alvo.position) < distanciaParaVerOAlvo)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidadeDeMovimento * Time.deltaTime);
            transform.LookAt(alvo);
        }
        else if (Vector3.Distance(transform.position, alvo.position) <= distanciaParaParar && (Vector3.Distance(transform.position, alvo.position) > distanciaParaRecuar))
        {
            transform.position = transform.position;
            transform.LookAt(alvo);
        }
        else if(Vector3.Distance(transform.position, alvo.position) < distanciaParaRecuar)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, -velocidadeDeMovimento * Time.deltaTime);
            transform.LookAt(alvo);
        }
    }
    
}
