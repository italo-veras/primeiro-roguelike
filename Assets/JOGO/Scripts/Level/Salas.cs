using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salas : MonoBehaviour
{
    public List<GameObject> inimigos;
    public List<GameObject> player;
    public Collider[] portas;
    public GameObject[] recompensa;
    public Transform caixa;
    

    public LayerMask playerMask;
    void Start()
    {
       
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ChecarPlayer();
        ChecarInimigo();
        AbrirPorta();
    }
    void ChecarInimigo()
    {
        for (int i = 0; i < inimigos.Count; i++)
        {

            if (inimigos[i] == null)
            {
                inimigos.Remove(inimigos[i]);
            }
        }
    }
    void ChecarPlayer()
    {
        player.Clear();

        Collider[] atingiuPlayer = Physics.OverlapBox(caixa.position, caixa.localScale / 2, Quaternion.identity, playerMask);

        for (int i = 0; i < atingiuPlayer.Length; i++)
        {
            GameObject playerAtingido = atingiuPlayer[i].gameObject;

            player.Add(playerAtingido);
        }

    }
    void AbrirPorta()
    {
        for (int i = 0; i < portas.Length; i++)
        {
            if (inimigos.Count > 0 && player.Count == 1)
            {
                portas[i].isTrigger = false;
            }
            if (inimigos.Count == 0)
            {
                portas[i].isTrigger = true;
            }
        }
    }
}
