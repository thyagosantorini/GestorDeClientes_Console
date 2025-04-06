using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeClientes_Console
{
    internal class Program
    {

        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Adicionar =1, Remover = 2, Listagem = 3, Sair }

        static void Main(string[] args)
        {

            Carregar();
            bool opcaoSair = false;

            while (!opcaoSair) 
            {
                Console.WriteLine("Sistema de Clientes - Seja Bem vindo(a)!");
                Console.WriteLine("1 - Adicionar\n2 - Remover\n3 - Listagem\n4 - Sair");
                int numeroOpcao = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------");
                Menu opcao = (Menu)numeroOpcao;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Sair:
                        opcaoSair = true;
                        break;
                }
                Console.Clear();

            }
         
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("Cadastro concluído com sucesso! Aperte Enter para Sair");
            Console.ReadLine();

        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }

        static void Listagem()
        {

            if(clientes.Count > 0)
            {
                Console.WriteLine("Lista de Clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine("Lista de clientes: ");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("============================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nehum Cliente cadastrado!");
            }
            

            Console.WriteLine("Aperte Enter Continuar!");
            Console.ReadLine();

        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat",FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat",FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
            }
            

            stream.Close();

        }

    }
}
