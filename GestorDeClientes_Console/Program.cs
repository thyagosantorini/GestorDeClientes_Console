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
        // Marca a struct como serializável, ou seja, pode ser salva em arquivo
        [System.Serializable]
        struct Cliente
        {
            public string nome; // Nome do Cliente
            public string email; // E-mail do cliente
            public string cpf; // CPF do cliente
        }

        // Lista que armazena os Clientes cadastrados
        static List<Cliente> clientes = new List<Cliente>();
        
        // Enumeração com as opões do Menu (Substitui números por nome)
        enum Menu { Adicionar =1, Remover = 2, Listagem = 3, Sair }

        // Método principal que roda o programa
        static void Main(string[] args)
        {

            Carregar(); // Carrega dados do arquivo para a Lista(Se existeirem!)
            bool opcaoSair = false; // Controle para manter o programa rodando até o usuário escolher Sair.

            while (!opcaoSair) // Enquanto o usuário não escolher sair
            {
                // Mostra o Menu Principal
                Console.WriteLine("Sistema de Clientes - Seja Bem vindo(a)!");
                Console.WriteLine("1 - Adicionar\n2 - Remover\n3 - Listagem\n4 - Sair");

                // Lê a opção digitada e converter para inteiro
                int numeroOpcao = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------");

                // Converte o número digitado para o tipo Menu
                Menu opcao = (Menu)numeroOpcao;

                // Executa a ação de acordo com a opção escolhida
                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar(); // Chama método para adicionar cliente
                        break;
                    case Menu.Remover:
                        Remover(); // Chama método para remover cliente
                        break;
                    case Menu.Listagem:
                        Listagem(); // Chama método para listar clientes
                        break;
                    case Menu.Sair:
                        opcaoSair = true; // Sai do loop e encerra o programa
                        break;
                }
                Console.Clear(); // Limpa o console depois de cada ação

            }
         
        }

        // Método que adiciona um novo cliente
        static void Adicionar()
        {
            Cliente cliente = new Cliente(); // Cria um novo cliente

            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine(); // Lê o nome

            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine(); // Lê o email

            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine(); // Lê o CPF

            clientes.Add(cliente); // Adiciona o cliente na lista
            Salvar(); // Salva a lista atualizada no arquivo

            Console.WriteLine("Cadastro concluído com sucesso! Aperte Enter para Sair");
            Console.ReadLine(); // Espera o usuário pressionar Enters

        }

        // Método que remove um cliente pelo ID
        static void Remover()
        {
            Listagem();// Mostra os clientes antes de remover

            Console.WriteLine("Digite o ID do cliente que você quer remover: ");
            int id = int.Parse(Console.ReadLine()); // Lê o ID informado

            // Verifica se o ID é válido (dentro dos limites da lista)
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id); // Remove o cliente da lista
                Salvar(); // Salva a lista atualizada
            } 
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!");
                Console.ReadLine(); // Aguarda o Enter
            }
        }

        // Método que lista todos os clientes
        static void Listagem()
        {
            // Verifica se existe pelo menos um cliente cadastrado
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de Clientes: ");
                int i = 0; // Usado para mostrar o ID de cada cliente

                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine("Lista de clientes: ");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("============================");
                    i++; // Incrementa o ID
                }
            }
            else
            {
                Console.WriteLine("Nehum Cliente cadastrado!"); // Mensagem se não houver clientes
            }
            

            Console.WriteLine("Aperte Enter Continuar!");
            Console.ReadLine();

        }

        // Método que salva os dados no arquivo
        static void Salvar()
        {
            // Cria um fluxo de arquivo chamado "clients.dat"
            FileStream stream = new FileStream("clients.dat",FileMode.OpenOrCreate);

            BinaryFormatter enconder = new BinaryFormatter(); // Cria o serializador binário

            enconder.Serialize(stream, clientes); // Salva a lista no arquivo

            stream.Close(); // Fecha o arquivo
        }

        // Método que carrega os dados do arquivo para a lista
        static void Carregar()
        {
            // Tenta abrir o arquivo (se não existir, ele cria)
            FileStream stream = new FileStream("clients.dat",FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter enconder = new BinaryFormatter(); // Cria o desserializador

                // Tenta ler o arquivo e transformar de volta na lista
                clientes = (List<Cliente>)enconder.Deserialize(stream);

                // Se o conteúdo for nulo, cria uma nova lista
                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch (Exception e) // Se ocorrer algum erro (ex: arquivo vazio)
            {
                clientes = new List<Cliente>(); // Cria uma nova lista vazia
            }
            

            stream.Close(); // Fecha o arquivo.

        }

    }
}
