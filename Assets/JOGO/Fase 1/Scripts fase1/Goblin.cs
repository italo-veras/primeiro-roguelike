using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseInimigo
{
    public Transform pontoDeDescanço;
    private Transform pontoAlvo;

    private void Awake()
    {
        pontoAlvo = pontoDeDescanço;
        pontoDeDescanço = GetComponent<Transform>();
    }
    protected override void Mover()
    {
        base.Mover();
        if (Vector3.Distance(transform.position, alvo.position) > distanciaParaVerOAlvo)
        {
            if (Vector3.Distance(transform.position, pontoDeDescanço.position) < 10)
            {

                transform.LookAt(alvo.position);
            }
            
            transform.LookAt(pontoAlvo);
            transform.position = Vector3.MoveTowards(transform.position, pontoAlvo.position, velocidadeDeMovimento * Time.deltaTime);
        }
    }
}
