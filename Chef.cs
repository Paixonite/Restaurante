using System;
using System.Collections.Concurrent;
using System.Threading;

public class Chef
{
    private int idChefe;
    private string[] nomesPratos = {"executivo", "italiano", "especial"};
    private BlockingCollection<(int pedido, int prato)> pedidos;

    static object arrozLock = new object();
    static object carneLock = new object();
    static object macarraoLock = new object();
    static object molhoLock = new object();

    static Ingrediente arroz = new Ingrediente(3);
    static Ingrediente carne = new Ingrediente(2);
    static Ingrediente macarrao = new Ingrediente(4);
    static Ingrediente molho = new Ingrediente(2);


    public Chef(int idChefe, BlockingCollection<(int pedido, int prato)> pedidos)
    {
        this.idChefe = idChefe;
        this.pedidos = pedidos;
    }

    public void Trabalhar()
    {
        Program.ConsoleLock($"[Chef {idChefe}] Estou pronto!", ConsoleColor.Red);

        foreach (var (pedido, prato) in pedidos.GetConsumingEnumerable())
        {
            Program.ConsoleLock($"[Chef {idChefe}] Iniciando o prato {nomesPratos[prato-1]} do pedido {pedido}!", ConsoleColor.Red);

            fazPrato(prato);

            Program.ConsoleLock($"[Chef {idChefe}] Finalizei o prato {nomesPratos[prato-1]} do pedido {pedido}!", ConsoleColor.Red);
        }
    }

    private void fazPrato(int tipo)
    {
        switch(tipo)
        {
            case 1:
                lock(arrozLock){
                    if (arroz.quantidade == 0){
                        Program.ConsoleLock($"[Chef {idChefe}] Preparando arroz!", ConsoleColor.Green);
                        arroz.preparar();
                        Program.ConsoleLock($"[Chef {idChefe}] Arroz preparado! Estoque: {arroz.quantidade} porções", ConsoleColor.Green);
                    }
                        arroz.quantidade--;
                }

                lock(carneLock){
                    if (carne.quantidade == 0){
                        Program.ConsoleLock($"[Chef {idChefe}] Preparando carne!", ConsoleColor.Green);
                        carne.preparar();
                        Program.ConsoleLock($"[Chef {idChefe}] Carne preparada! Estoque: {carne.quantidade} porções", ConsoleColor.Green);
                    }
                        carne.quantidade--;
                }

                arroz.montar();
                carne.montar();
                break;

            case 2:
                lock(macarraoLock){
                    if (macarrao.quantidade == 0){
                        Program.ConsoleLock($"[Chef {idChefe}] Preparando macarrao!", ConsoleColor.Green);
                        macarrao.preparar();
                        Program.ConsoleLock($"[Chef {idChefe}] Macarrão preparado! Estoque: {macarrao.quantidade} porções", ConsoleColor.Green);
                    }
                        macarrao.quantidade--;
                }

                lock(molhoLock){
                    if (molho.quantidade == 0){
                        Program.ConsoleLock($"[Chef {idChefe}] Preparando molho!", ConsoleColor.Green);
                        molho.preparar();
                        Program.ConsoleLock($"[Chef {idChefe}] Molho preparado! Estoque: {molho.quantidade} porções", ConsoleColor.Green);
                    }
                        molho.quantidade--;
                }

                macarrao.montar();
                molho.montar();
                break;
                
            case 3:
                lock(arrozLock)
                {
                    if (arroz.quantidade == 0)
                    {
                        Program.ConsoleLock($"[Chef {idChefe}] Preparando arroz!", ConsoleColor.Green);
                        arroz.preparar();
                        Program.ConsoleLock($"[Chef {idChefe}] Arroz preparado! Estoque: {arroz.quantidade} porções", ConsoleColor.Green);
                    }
                        arroz.quantidade--;
                }

                lock(carneLock){
                    if (carne.quantidade == 0){
                        Program.ConsoleLock($"[Chef {idChefe}] Preparando carne!", ConsoleColor.Green);
                        carne.preparar();
                        Program.ConsoleLock($"[Chef {idChefe}] Carne preparada! Estoque: {carne.quantidade} porções", ConsoleColor.Green);
                    }
                        carne.quantidade--;
                }
                lock(molhoLock){
                    if (molho.quantidade == 0){
                        Program.ConsoleLock($"[Chef {idChefe}] Preparando molho!", ConsoleColor.Green);
                        molho.preparar();
                        Program.ConsoleLock($"[Chef {idChefe}] Molho preparado! Estoque: {molho.quantidade} porções", ConsoleColor.Green);
                    }
                        molho.quantidade--;
                }

                arroz.montar();
                carne.montar();
                molho.montar();
                break;
        }
    }
}
