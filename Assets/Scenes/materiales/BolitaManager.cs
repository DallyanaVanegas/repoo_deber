using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolitaManager : MonoBehaviour
{
   public GameObject comida;
    public int cantidadComida = 50;
    public AgenteBolita agente;
    public int cantidadAgente = 10;

    public ParasitoBolita parasito;

    public  List<GameObject> alimentos = new List<GameObject>();
    public  List<AgenteBolita> agentes = new List<AgenteBolita>();
    public  List<ParasitoBolita> parasitos = new List<ParasitoBolita>();

    private int conteo = 0;
    private float timer = 0;

    void Start()
    {
         AdicionaParasito(transform.position);
        
        //creamos la comida
        for(int n=0 ; n<cantidadComida;n++){
            GameObject temp = Instantiate (comida,
            new Vector3(Random.Range(-50,50), Random.Range(-50,50),
            Random.Range(-50,50)), Quaternion.identity);

            alimentos.Add(temp);
        }

        //creamos las bolitas
        for(int n=0 ; n<cantidadAgente; n++){
            AgenteBolita temp = Instantiate ( agente,
            new Vector3(Random.Range(-50,50), Random.Range(-50,50),
            Random.Range(-50,50)), Quaternion.identity);

            temp.PonID(conteo);
            conteo++;
        
        //colocamnos los gnes
        //tamano
        temp.PonGen(0,Random.Range(1.5f,3.5f));
        //color
        temp.PonGen(1,Random.Range(0.0f,1.0f));
        temp.PonGen(2,Random.Range(0.0f,1.0f));
        temp.PonGen(3,Random.Range(0.0f,1.0f));
        //velocidad
        temp.PonGen(4,Random.Range(5,15));
        //rango de vision
        temp.PonGen(5,Random.Range(20,150));
        //costo
        temp.PonGen(6,Random.Range(0.5f,2.0f));
        agentes.Add(temp);
        }
    }

    public void AdicionaAlimento(Vector3 pPosicion){
        if(alimentos.Count<100){
            GameObject temp = Instantiate(comida, pPosicion, Quaternion.identity);
            alimentos.Add(temp);
        }
    }

    public void AdicionaParasito(Vector3 pPosicion){
        //ParasitoBolita temp = Instantiate(parasito, pPosicion, Quaternion.identity);
        if(parasitos.Count<3){
            
            for(int n=0 ; n<1;n++){
                ParasitoBolita res = Instantiate (parasito,
                new Vector3(Random.Range(-50,50), Random.Range(-50,50),
                Random.Range(-50,50)), Quaternion.identity);

                res.PonID(conteo);
                conteo++;
            
                //colocamnos los gnes
                //tamano
                res.PonGen(0,4.0f);
                //velocidad
                res.PonGen(1,5);
                //rango de vision
                res.PonGen(2,10);

                parasitos.Add(res);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(alimentos.Count + ", " + agentes.Count);
        timer += Time.deltaTime;

        if(timer>=0.5)
        {
            timer = 0;
            AdicionaAlimento(new Vector3(Random.Range(-50,50),Random.Range(-50,50),Random.Range(-50,50)));
        }
    }
    public void Cruce(AgenteBolita p1, AgenteBolita p2){
        if(agentes.Count > 100) return;

        int propRecombinacion = 70;
        int propMutacion = 10;

        if(Random.Range(1,100)< propRecombinacion){
            //seleccionamos el punto de corte
            int c = Random.Range(0,7);
            int n=0;
            AgenteBolita temp = Instantiate(agente, Vector3.zero, Quaternion.identity);

            temp.PonID(conteo);
            conteo++;

            //copiamos de padre1 a hijo
            for(n=0; n<c; n++){
                temp.PonGen(n, p1.ObtenGen(n));
            }
            //copiamos de padre2 a hijo
            for(n=c; n<7; n++){
                temp.PonGen(n, p2.ObtenGen(n));
            }
            //vemos si hay mutacion
            if(Random.Range(0,100)<propMutacion){
                //seleccionamos el gen
                int gen= Random.Range(0,7);
                if(gen == 0)
                temp.PonGen(0, Random.Range(1.5f,3.5f));
                else if( gen==1)
                temp.PonGen(1,Random.Range(0.0f,1.0f));
                 else if( gen==2)
                temp.PonGen(2,Random.Range(0.0f,1.0f));
                else if( gen==3)
                temp.PonGen(3,Random.Range(0.0f,1.0f));
                 else if( gen==4)
                temp.PonGen(4,Random.Range(5,15));
                else if( gen==5)
                temp.PonGen(5,Random.Range(20,150));
                else if( gen==6)
                temp.PonGen(6,Random.Range(0.5f,2.0f));
            }
            temp.energia = 500;
            p1.energia = 500;
            p2.energia = 500;

            agentes.Add(temp);
        } else{
            AgenteBolita temp= Instantiate(agente,
            Vector3.zero, 
            Quaternion.identity);

            temp.PonID(conteo);
            conteo++;

            //colocamos los genes
            //tamano 
            temp.PonGen(0,p1.ObtenGen(0));
            //color
            temp.PonGen(1,p1.ObtenGen(1));
            temp.PonGen(2,p1.ObtenGen(2));
            temp.PonGen(3,p1.ObtenGen(3));
             //velocidad
            temp.PonGen(4,p1.ObtenGen(4));
             //rango de vision
            temp.PonGen(5,p1.ObtenGen(5));
             //costo
            temp.PonGen(6,p1.ObtenGen(6));
            temp.energia = 500;
            p1.energia = 500;
            p2.energia = 500;

            agentes.Add(temp);
        }
    }
}

