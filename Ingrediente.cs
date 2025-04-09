
using System;
using System.Threading;

public class Ingrediente 
{
    public int quantidade;
    public int porcoesPorPreparo;
    
    public Ingrediente(int porcoesPorPreparo)
    {
        this.quantidade = 0;
        this.porcoesPorPreparo = porcoesPorPreparo;
    }

    public void preparar()
    {
        Thread.Sleep(2_000);
        quantidade += porcoesPorPreparo;
    }

    public void montar(){
        Thread.Sleep(1_000);
    }
}