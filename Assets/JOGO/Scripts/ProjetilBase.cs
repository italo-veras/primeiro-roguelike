using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilBase : MonoBehaviour
{
    [SerializeField] private Rigidbody meuRb;
    [SerializeField] private Transform alvo,alvoAlternativo;
    [SerializeField] protected private float velocidadeDoProjetil;

    [SerializeField] protected private float vidaMaxima;
    private float vidaPorTempo;

    public GameObject efeitoDeDestruir;

    private Vector3 posicaoFixaDoPlayer,posicaoFixaDoAlvoAlternativo;

    void Start()
    {
        
        
        alvo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        alvoAlternativo = GameObject.FindGameObjectWithTag("Inimigo").GetComponent<Transform>();

        posicaoFixaDoAlvoAlternativo = new Vector3(alvoAlternativo.position.x, alvoAlternativo.position.y, alvoAlternativo.position.z);

        posicaoFixaDoPlayer = new Vector3(alvo.position.x, alvo.position.y, alvo.position.z);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // vida da bala // 
        vidaPorTempo += Time.deltaTime;

        if (vidaPorTempo >= vidaMaxima)
        {
            // Instantiate(efeitoDeDestruir, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (alvoAlternativo == null)
        {
            Destroy(gameObject,2);
        }
        else if (alvo == null)
        {
            Destroy(gameObject,2);
        }

        BalaIndoReto();
        BalaSeguindo();
        BalaSeguindoAlvoAlternativo();
        BalaIndoRetoNaPosicaoDoAlvoAlternativo();

      
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            // Instantiate(efeitoDeDestruir, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } 
        else if(other.tag == "Mapa")
        {
            Destroy(this.gameObject);

        }else if (other.tag == "Inimigo")
        {
            Destroy(this.gameObject);

        }


    }
    
    protected  virtual void BalaSeguindo()
    {
        transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidadeDoProjetil *Time.deltaTime);
    }
    protected  virtual void BalaIndoReto()
    {
        transform.position = Vector3.MoveTowards(transform.position, posicaoFixaDoPlayer, velocidadeDoProjetil * Time.deltaTime);
        if((transform.position.x == posicaoFixaDoPlayer.x) && (transform.position.z == posicaoFixaDoPlayer.z))
        {
            Destroy(this.gameObject);
        }
  
    }
    protected virtual void BalaSeguindoAlvoAlternativo()
    {
        transform.position = Vector3.MoveTowards(transform.position,alvoAlternativo.position, velocidadeDoProjetil * Time.deltaTime);

    }
    protected virtual void BalaIndoRetoNaPosicaoDoAlvoAlternativo()
    {
        transform.position = Vector3.MoveTowards(transform.position, posicaoFixaDoAlvoAlternativo, velocidadeDoProjetil * Time.deltaTime);
        if ((transform.position.x == posicaoFixaDoAlvoAlternativo.x) && (transform.position.z == posicaoFixaDoAlvoAlternativo.z))
        {
            Destroy(this.gameObject);
        }

    }

}
