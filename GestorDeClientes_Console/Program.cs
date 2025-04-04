using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeClientes_Console
{
    internal class Program
    {

        enum Menu { Adicionar =1, Remover, Listagem, Sair }

        static void Main(string[] args)
        {
            bool opcaoSair = false;

            while (!opcaoSair) 
            {
                Console.WriteLine("Sistema de Clientes - Seja Bem vindo(a)!");
                Console.WriteLine("1 - Adicionar\n2 - Remover\n3 - Listagem\n4 - Sair");
                int numeroOpcao = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)numeroOpcao;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        break;
                    case Menu.Remover:
                        break;
                    case Menu.Listagem:
                        break;
                    case Menu.Sair:
                        opcaoSair = true;
                        break;
                }
                Console.Clear();
            }
         
        }
    }
}
