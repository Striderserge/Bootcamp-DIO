using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ProgramaPet1._01
{
    class Program
    {
        //pensar como fazer o  balanço
        //melhoria - validação de dados no cadastro
        //melhoria - criar um procedimento para fazer a validação dos dados ao inves de deixar em cada método para economizar linhas.
        public struct Fornecedor
        {
            public int idforn; // id fornecedor
            public double cnpj;
            public int tel; 
            public double ie; //inscrição estadual
            public string razaosocial;
            public string nomefantasia;
            public string end; // endereço
            public string email;           
        }
        public struct Cliente
        {
            public int idcli; // id cliente
            public double cpf;
            public double rg;
            public int tel; 
            public string nome;
            public string end; // endereço
            public string email;           
        }
        public struct Produto
        {
            public int idprod; // id produto
            public double qtdin; //quantidade entrada
            public double qtdout; //quantidade saida
            public double qtdest; // quantidade do estoque (soma na entrada e subtrai na saida)
            public double qtdvend; // quantidade vendida;
            public double custo_compra;
            public double custo_venda;
            public double custo_medio_compra;
            //public double custo_medio_venda; usarei quando fazer balanço
            public string nome;
            public string categoria; // tipo de produto            
        }      
        static void Main(string[] args)
        {
            Fornecedor[] fornecedor = new Fornecedor[50];
            Cliente[] cliente = new Cliente[50];
            Produto[] produto = new Produto[50];
            int opcao,dadosf,dadosc,dadosp;
            dadosf = CarregarDadosF(fornecedor);
            dadosc = CarregarDadosC(cliente);
            dadosp = CarregarDadosP(produto);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();        
            Senha();
            do
            {
                opcao = Menu();
                switch (opcao)
                {
                    case 0:
                        SalvarDadosF(fornecedor);
                        SalvarDadosC(cliente);
                        SalvarDadosP(produto);
                        break;
                    case 1:
                        CadFornecedor(fornecedor);
                        break;
                    case 2:
                        CadCliente(cliente);
                        break;
                    case 3:
                        CadProduto(produto);
                        break;                    
                    case 4:
                        EntProduto(fornecedor,produto);
                        break;
                    case 5:
                        OutProduto(cliente, produto);
                        break;
                    case 6:
                        Estoque(produto);
                        break;
                }
            } while (opcao != 0);
            Thread.Sleep(1500);
        }
        static void Senha()
        {
            string user, pass;
            Console.WriteLine("\nV1.09\t\t\t──────▄▀▄─────▄▀▄");
            Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
            Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
            Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
            Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\t\t\t\n\n\n");
            Console.Write("\t\t\tUsuario : ");
            user = Console.ReadLine().ToUpper();
            System.Console.Write("\t\t\tSenha : ");            
            pass = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                pass += key.KeyChar;
            }
            while ((user != "SERGIO") || (pass != "1234"))
            {
                Console.WriteLine("\n\n\t\t\tUsuario ou Senha invalidos");
                Thread.Sleep(1500);
                Console.Clear();
                Console.WriteLine("\nV1.09\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n\n");
                Console.Write("\t\t\tUsuario : ");
                user = Console.ReadLine().ToUpper();
                System.Console.Write("\t\t\tSenha : ");
                pass = null;
                while (true)
                {
                    var key = System.Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    pass += key.KeyChar;
                }
            }
            Console.WriteLine("\n\n\t\t\tAcesso Permitido");
            Thread.Sleep(1500);
            Console.Clear();
        }        
        static int Menu()
        {
            int escolha;
            Console.Clear();
            Console.WriteLine("\n\n\t\t\t──────▄▀▄─────▄▀▄");
            Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
            Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
            Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
            Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n");

            Console.WriteLine("\t\t\t------ Menu de Opções ------");
            Console.WriteLine("\t\t\t|                           |");
            Console.WriteLine("\t\t\t| 1 - Cadastrar Fornecedor  |");
            Console.WriteLine("\t\t\t| 2 - Cadastrar Cliente     |");
            Console.WriteLine("\t\t\t| 3 - Cadastrar Produtos    |");
            Console.WriteLine("\t\t\t| 4 - Entrada de Produtos   |");
            Console.WriteLine("\t\t\t| 5 - Saida de Produtos     |");
            Console.WriteLine("\t\t\t| 6 - Controle de Estoque   |");
            Console.WriteLine("\t\t\t| 0 - Sair do Sistema       |");
            Console.WriteLine("\t\t\t-----------------------------");
            Console.Write("\n\n\n\n\nIndique o número da opção desejada: ");
            while (!(int.TryParse(Console.ReadLine(), out escolha) && (escolha >= 0 && escolha <= 6)))
                Console.Write("Opção inválida!! Digite número entre 0 e 6: ");
            return escolha;
        }
        static void CadFornecedor(Fornecedor[] fornecedor)
        {
            int i = 1, add;
            string confirmar = "";
            string cancela = "N";
            Console.Clear();

            while ((fornecedor[i].nomefantasia != null) && (i < 50))
            {
                i++;
            }
            if (i > 1)
                add = fornecedor[i - 1].idforn + 1;
            else
                add = 1;
            do
            {
                if (confirmar == "N")
                    Console.Clear();
                Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n\n");
                Console.WriteLine("\t\t\t- -  Cadastro de Fornecedor  - -\n\n");
                Console.WriteLine("\nFornecedor código {0} ", add);
                fornecedor[i].idforn = add;
                Console.Write("Informe o nome do fornecedor : ");
                fornecedor[i].nomefantasia = Console.ReadLine();
                Console.Write("Informe a Razao Social : ");
                fornecedor[i].razaosocial = Console.ReadLine();
                Console.Write("Informe o CNPJ : ");
                while (!(double.TryParse(Console.ReadLine(), out fornecedor[i].cnpj)))
                    Console.Write("CNPJ Invalido, por favor digite novamente : ");
                Console.Write("Informe a Inscricao Estadual : ");
                while (!(double.TryParse(Console.ReadLine(), out fornecedor[i].ie)))
                    Console.Write("I.E. Invalido, por favor digite novamente : ");
                Console.Write("Informe o Endereço : ");
                fornecedor[i].end = Console.ReadLine();
                Console.Write("Informe o Telefone : ");
                while (!(int.TryParse(Console.ReadLine(), out fornecedor[i].tel)))
                    Console.Write("telefone Invalido, por favor digite novamente : ");
                Console.Write("Informe o e-mail : ");
                fornecedor[i].email = Console.ReadLine();
                Console.Write("\n\nConfirma a gravaçao dos dados?(S/N) : ");
                confirmar = Console.ReadLine().ToUpper();
                while (!((confirmar == "S") || (confirmar == "N")))
                {
                    Console.Write("Opcao invalida! Digite (S/N) : ");
                    confirmar = Console.ReadLine().ToUpper();
                }
                if(confirmar =="N")
                {
                    Console.Write("Deseja cancelar o cadastro? (S/N) : ");
                    cancela = Console.ReadLine().ToUpper();
                    while (!((cancela == "S") || (cancela == "N")))
                    {
                        Console.Write("Opcao invalida! Digite (S/N) : ");
                        cancela = Console.ReadLine().ToUpper();
                    }
                }
                if (cancela == "S")
                    break;
            } while (confirmar != "S");
            if (cancela == "S")
            {
                fornecedor[i].nomefantasia = null;
                i--;
                Console.Write("Cadastro Cancelado!");
            }
            else
                Console.WriteLine("Cadastro realizado com sucesso!");
            Thread.Sleep(1000);
        }
        static void CadCliente(Cliente[] cliente)
        {
            int i = 1, add;
            string confirmar = "";
            string cancela = "";
            Console.Clear();
            while ((cliente[i].nome != null) && (i < 50))
            {
                i++;
            }
            if (i > 1)
                add = cliente[i - 1].idcli + 1;
            else
                add = 1;
            do
            {
                if (confirmar == "N")
                    Console.Clear();
                Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n\n");
                Console.WriteLine("\t\t\t- -  Cadastro de Cliente  - -\n\n");
                Console.WriteLine("\nCliente código {0} ", add);
                cliente[i].idcli = add;
                Console.Write("Informe o nome do Cliente : ");
                cliente[i].nome = Console.ReadLine();
                Console.Write("Informe o CPF : ");
                while (!(double.TryParse(Console.ReadLine(), out cliente[i].cpf)))
                    Console.Write("CPF Invalido, por favor digite novamente : ");
                Console.Write("Informe o RG : ");
                while (!(double.TryParse(Console.ReadLine(), out cliente[i].rg)))
                    Console.Write("RG Invalido, por favor digite novamente : ");
                Console.Write("Informe o Endereço : ");
                cliente[i].end = Console.ReadLine();
                Console.Write("Informe o Telefone : ");
                while (!(int.TryParse(Console.ReadLine(), out cliente[i].tel)))
                    Console.Write("telefone Invalido, por favor digite novamente : ");
                Console.Write("Informe o e-mail : ");
                cliente[i].email = Console.ReadLine();
                Console.Write("\n\nConfirma a gravaçao dos dados?(S/N) : ");
                confirmar = Console.ReadLine().ToUpper();
                while (!((confirmar == "S") || (confirmar == "N")))
                {
                    Console.Write("Opcao invalida! Digite (S/N) : ");
                    confirmar = Console.ReadLine().ToUpper();
                }
                if (confirmar == "N")
                {
                    Console.Write("Deseja cancelar o cadastro? (S/N) : ");
                    cancela = Console.ReadLine().ToUpper();
                    while (!((cancela == "S") || (cancela == "N")))
                    {
                        Console.Write("Opcao invalida! Digite (S/N) : ");
                        cancela = Console.ReadLine().ToUpper();
                    }
                }
                if (cancela == "S")
                    break;
            } while (confirmar != "S");
            if (cancela == "S")
            {
                cliente[i].nome = null;
                i--;
                Console.Write("Cadastro Cancelado!");
            }
            else
                Console.WriteLine("Cadastro realizado com sucesso!");
            Thread.Sleep(1000);
        }
        static void CadProduto(Produto[] produto)
        {
            int i = 1, add;
            string confirmar = "";
            string cancela = "";
            Console.Clear();
            while ((produto[i].nome != null) && (i < 50))
            {
                i++;
            }
            if (i > 1)
                add = produto[i - 1].idprod + 1;
            else
                add = 1;
            do
            {
                if (confirmar == "N")
                    Console.Clear();
                Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n\n");
                Console.WriteLine("\t\t\t- -  Cadastro de Produto  - -\n\n");
                Console.WriteLine("\nProduto código {0} ", add);
                produto[i].idprod = add;
                Console.Write("Informe o nome do Produto : ");
                produto[i].nome = Console.ReadLine();
                Console.Write("Informe a categoria do produto : ");
                produto[i].categoria = Console.ReadLine();
                Console.Write("\n\nConfirma a gravaçao dos dados?(S/N) : ");
                confirmar = Console.ReadLine().ToUpper();
                while (!((confirmar == "S") || (confirmar == "N")))
                {
                    Console.Write("Opcao invalida! Digite (S/N) : ");
                    confirmar = Console.ReadLine().ToUpper();
                }
                if (confirmar == "N")
                {
                    Console.Write("Deseja cancelar o cadastro? (S/N) : ");
                    cancela = Console.ReadLine().ToUpper();
                    while (!((cancela == "S") || (cancela == "N")))
                    {
                        Console.Write("Opcao invalida! Digite (S/N) : ");
                        cancela = Console.ReadLine().ToUpper();
                    }
                }
                if (cancela == "S")
                    break;
            } while (confirmar != "S");
            if (cancela == "S")
            {
                produto[i].nome = null;
                i--;
                Console.Write("Cadastro Cancelado!");
            }
            else
                Console.WriteLine("Cadastro realizado com sucesso!");
            Thread.Sleep(1000);
        }
        static int ListaFornecedor(Fornecedor[] fornecedor)
        {
            int i = 1;
            int r = 0;
            int escolha;
            string loop = "";
            Console.Clear();
            Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
            Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
            Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
            Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
            Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n");
            Console.WriteLine("\t\t- - Resumo dos Fornecedores Cadastrados - - ");
            Console.Write("********************************************************************************");

            Console.WriteLine("ID\tNome");
            while ((i < 50))
            {
                if (fornecedor[i].nomefantasia != null)
                {
                    Console.Write("{0}\t", fornecedor[i].idforn);
                    Console.Write("{0}\n", fornecedor[i].nomefantasia);
                }
                i++;
            }
            Console.Write("********************************************************************************");

            Console.Write("\nSelecione a empresa : ");
            int.TryParse(Console.ReadLine(), out escolha); 
            while(escolha>=50)
            {
                Console.Write("Valor fora do paramentro! tente novamente : ");
                int.TryParse(Console.ReadLine(), out escolha);
            }
            while (!(fornecedor[escolha].nomefantasia != null))
            {
                Console.Write("Opção inválida! selecione novamente : ");
                int.TryParse(Console.ReadLine(), out escolha);
                while (escolha >= 50)
                {
                    Console.Write("Valor fora do paramentro! tente novamente : ");
                    int.TryParse(Console.ReadLine(), out escolha);
                }
                r++;
                if ((r > 2) && (fornecedor[escolha].nomefantasia == null))
                {
                Console.Write("Deseja cancelar?(S/N) : ");
                    loop = Console.ReadLine().ToUpper();
                    while (!((loop=="S")||(loop=="N")))
                    {
                        Console.Write("Opcao inválida!Deseja Cancelar?(S/N) : ");
                        loop = Console.ReadLine().ToUpper();
                    }
                    if (loop == "S")
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("\nSelecione a Empresa : ");
                        int.TryParse(Console.ReadLine(), out escolha);
                        while (escolha >= 50)
                        {
                            Console.Write("Valor fora do paramentro! tente novamente : ");
                            int.TryParse(Console.ReadLine(), out escolha);
                        }
                    }
                }
            } return escolha;
        }
        static int ListaCliente(Cliente[] cliente)
        {
            int i = 1;
            int r = 0;
            int escolha;
            string loop = "";
            Console.Clear();
            Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
            Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
            Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
            Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
            Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n");
            Console.WriteLine("\t\t- - Resumo dos Clientes Cadastrados - - ");
            Console.Write("********************************************************************************");
            Console.WriteLine("ID\tNome\t\t\tRG");
            while ((i < 50))
            {
                if (cliente[i].nome != null)
                {
                    Console.Write("{0}\t", cliente[i].idcli);
                    Console.Write("{0}\t\t\t", cliente[i].nome);
                    Console.Write("{0}\n", cliente[i].rg);
                }
                i++;
            }
            Console.Write("********************************************************************************");
            Console.Write("\nSelecione o Cliente : ");
            int.TryParse(Console.ReadLine(), out escolha);
            while (escolha >= 50)
            {
                Console.Write("Valor fora do paramentro! tente novamente : ");
                int.TryParse(Console.ReadLine(), out escolha);
            }
            while (!(cliente[escolha].nome != null))
            {
                Console.Write("Opção inválida! selecione novamente : ");
                int.TryParse(Console.ReadLine(), out escolha);
                while (escolha >= 50)
                {
                    Console.Write("Valor fora do paramentro! tente novamente : ");
                    int.TryParse(Console.ReadLine(), out escolha);
                }
                r++;
                if ((r > 2) && (cliente[escolha].nome == null))
                {
                    Console.Write("Deseja cancelar?(S/N) : ");
                    loop = Console.ReadLine().ToUpper();
                    while (!((loop == "S") || (loop == "N")))
                    {
                        Console.Write("Opcao inválida!Deseja Cancelar?(S/N) : ");
                        loop = Console.ReadLine().ToUpper();
                    }
                    if (loop == "S")
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("\nSelecione o Cliente : ");
                        int.TryParse(Console.ReadLine(), out escolha);
                        while (escolha >= 50)
                        {
                            Console.Write("Valor fora do paramentro! tente novamente : ");
                            int.TryParse(Console.ReadLine(), out escolha);
                        }
                    }
                }
            }
            return escolha;
        }
        static int ListaProduto(Produto[] produto)
        {
            int i = 1;
            int escolha;
            int r=0;
            string loop = "";
            Console.Clear();
            Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
            Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
            Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
            Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
            Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n");
            Console.WriteLine("\t\t- - Resumo dos Produtos Cadastrados - - ");
            Console.Write("********************************************************************************");
            Console.WriteLine("ID\tNome\t\t\tCategoria");
            while ((i < 50))
            {
                if (produto[i].nome != null)
                {
                    Console.Write("{0}\t", produto[i].idprod);
                    Console.Write("{0}\t\t\t", produto[i].nome);
                    Console.Write("{0}\n", produto[i].categoria);
                }
                i++;
            }
             Console.Write("********************************************************************************");
            Console.Write("\nSelecione o Produto : ");
            int.TryParse(Console.ReadLine(), out escolha);
            while (escolha >= 50)
            {
                Console.Write("Valor fora do paramentro! tente novamente : ");
                int.TryParse(Console.ReadLine(), out escolha);
            }
            while (!(produto[escolha].nome != null))
            {
                Console.Write("Opção inválida! selecione novamente : ");
                int.TryParse(Console.ReadLine(), out escolha);
                while (escolha >= 50)
                {
                    Console.Write("Valor fora do paramentro! tente novamente : ");
                    int.TryParse(Console.ReadLine(), out escolha);
                }
                r++;
                if ((r > 2)&&(produto[escolha].nome == null))
                {
                    Console.Write("Deseja cancelar?(S/N) : ");
                    loop = Console.ReadLine().ToUpper();
                    while (!((loop=="S")||(loop=="N")))
                    {
                        Console.Write("Opcao inválida!Deseja Cancelar?(S/N) : ");
                        loop = Console.ReadLine().ToUpper();
                    }
                    if (loop == "S")
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("\nSelecione o Produto : ");
                        int.TryParse(Console.ReadLine(), out escolha);
                        while (escolha >= 50)
                        {
                            Console.Write("Valor fora do paramentro! tente novamente : ");
                            int.TryParse(Console.ReadLine(), out escolha);
                        }
                    }
                }
            } return escolha;
        }
        static void EntProduto(Fornecedor[]fornecedor,Produto[]produto) //ja esta puxando herança
        {
            int i,j=0;
            string confirmar = "";
            string cancela = "";
            do
            {
                Console.Clear();
                if (confirmar == "N")
                    Console.Clear();
                Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n");
                Console.WriteLine("\t\t\t- -  Entrada de Produto  - -\n\n");
                Console.Write("Selecione o fornecedor : ");
                i = ListaFornecedor(fornecedor);
                if (fornecedor[i].nomefantasia == null)
                    break;
                Console.Write("Selecione o produto : ");
                j = ListaProduto(produto);
                if (produto[j].nome == null)                
                    break;                
                Console.Clear();
                Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n");
                Console.WriteLine("\t\t\t- -  Entrada de Produto  - -\n\n");
                Console.Write("********************************************************************************");
                Console.WriteLine("\t\t\tEmpresa : " + fornecedor[i].nomefantasia);
                Console.WriteLine("\t\t\tProduto : " + produto[j].nome);
                Console.Write("********************************************************************************");
                Console.Write("\nInforme a quantidade comprada : ");
                while (!(double.TryParse(Console.ReadLine(), out produto[j].qtdin)))
                    Console.Write("Valor invalido! Insira o valor correto : ");
                Console.Write("Informe o custo unitario de compra : ");
                while (!(double.TryParse(Console.ReadLine(), out produto[j].custo_compra)))
                    Console.Write("Valor invalido! Insira o valor correto : ");
                Console.Write("\n\nConfirma a gravaçao dos dados?(S/N) : ");
                confirmar = Console.ReadLine().ToUpper();
                while (!((confirmar == "S") || (confirmar == "N")))
                {
                    Console.Write("Opcao invalida! Digite (S/N) : ");
                    confirmar = Console.ReadLine().ToUpper();
                }
                if (confirmar == "N")
                {
                    Console.Write("Deseja cancelar o cadastro? (S/N) : ");
                    cancela = Console.ReadLine().ToUpper();
                    while (!((cancela == "S") || (cancela == "N")))
                    {
                        Console.Write("Opcao invalida! Digite (S/N) : ");
                        cancela = Console.ReadLine().ToUpper();
                    }
                }
                if (cancela == "S")
                    break;
            } while (confirmar != "S");

            if ((fornecedor[i].nomefantasia == null) || (produto[j].nome == null) || (cancela == "S"))
                Console.WriteLine("Cadastro Cancelado!");
            else
            {
                Console.WriteLine("Cadastro realizado com sucesso!");
                produto[j].custo_medio_compra = ((produto[j].custo_medio_compra * produto[j].qtdest) + (produto[j].custo_compra * produto[j].qtdin)) / (produto[j].qtdest + produto[j].qtdin);
                produto[j].qtdest = produto[j].qtdest + produto[j].qtdin;                
            }
            Thread.Sleep(1000);
        }
        static void OutProduto(Cliente[] cliente, Produto[] produto) //ja esta puxando herança
        {
            int i, j = 0;
            string confirmar = "";
            string cancela = "";
            do
            {                
                Console.Clear();
                if (confirmar == "N")
                    Console.Clear();
                Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n");
                Console.WriteLine("\t\t\t- -  Saida de Produto  - -");
                Console.Write("********************************************************************************");
                Console.Write("Selecione o fornecedor : ");
                i = ListaCliente(cliente);
                if (cliente[i].nome == null)
                    break;
                Console.Write("Selecione o produto : ");
                j = ListaProduto(produto);
                if (produto[j].nome == null)
                    break;
                Console.Clear();
                Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
                Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
                Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
                Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
                Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n");
                Console.WriteLine("\t\t\t- -  Saida de Produto  - -");
                Console.Write("********************************************************************************");
                Console.WriteLine("\t\t\tCliente : " + cliente[i].nome);
                Console.WriteLine("\t\t\tProduto : " + produto[j].nome);
                Console.Write("********************************************************************************");
                Console.Write("\nInforme a quantidade vendida : ");
                while (!(double.TryParse(Console.ReadLine(), out produto[j].qtdout)))
                    Console.Write("Valor inválido! Insira o valor correto : ");
                produto[j].qtdout = produto[j].qtdout * (-1);
                Console.Write("Informe o custo unitário de venda : ");
                while (!(double.TryParse(Console.ReadLine(), out produto[j].custo_venda)))
                    Console.Write("Valor inválido! Insira o valor correto : ");
                Console.Write("\n\nConfirma a gravaçao dos dados?(S/N) : ");
                confirmar = Console.ReadLine().ToUpper();
                while (!((confirmar == "S") || (confirmar == "N")))
                {
                    Console.Write("Opcao invalida! Digite (S/N) : ");
                    confirmar = Console.ReadLine().ToUpper();
                }
                if (confirmar == "N")
                {
                    Console.Write("Deseja cancelar o cadastro? (S/N) : ");
                    cancela = Console.ReadLine().ToUpper();
                    while (!((cancela == "S") || (cancela == "N")))
                    {
                        Console.Write("Opcao invalida! Digite (S/N) : ");
                        cancela = Console.ReadLine().ToUpper();
                    }
                }
                if (cancela == "S")
                    break;
            } while (confirmar != "S");

            if ((cliente[i].nome == null) || (produto[j].nome == null) || (cancela == "S"))
                Console.WriteLine("Cadastro Cancelado!");
            else
            {
                Console.WriteLine("Cadastro realizado com sucesso!");
                produto[j].qtdest = produto[j].qtdest + produto[j].qtdout;
                produto[j].qtdvend = produto[j].qtdvend + produto[j].qtdin;
            }
           //inserir aqui depois o balanço.
            Thread.Sleep(1000);
        }
        static void Estoque (Produto[]produto)
        {
            int i=0;
            Console.Clear();
            Console.WriteLine("\n\t\t\t──────▄▀▄─────▄▀▄");
            Console.WriteLine("\t\t\t─────▄█░░▀▀▀▀▀░░█▄");
            Console.WriteLine("\t\t\t─▄▄──█░░░░░░░░░░░█──▄▄");
            Console.WriteLine("\t\t\t█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
            Console.WriteLine("\n\t\t\t- - - C.A.T.S. Program - - -\n\n");
            Console.WriteLine("\t\t\t- - Controle de Estoque - - ");
            Console.WriteLine("\nID\tNome\tCategoria\tQtd\t\tValor Unit\tTotal");
            while ((i < 50))
            {
                if (produto[i].nome != null)
                {
                    Console.Write("{0}\t", produto[i].idprod);
                    Console.Write("{0}\t", produto[i].nome);
                    Console.Write("{0}\t\t", produto[i].categoria);
                    Console.Write("{0:N0}\t\t", produto[i].qtdest);
                    Console.Write("{0:N2}\t\t", produto[i].custo_medio_compra);
                    Console.Write("{0:N2}\n", (produto[i].custo_medio_compra*produto[i].qtdest));
                    Console.WriteLine("********************************************************************************");

                }
                i++;
            }
            Console.Write("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
        static int CarregarDadosF(Fornecedor[]fornecedor)
        {
            FileStream streamListar;
            if (!File.Exists(@"c:\orange\bancodadosf.bin")) // se o arquivo não existir, cria o arquivo
                streamListar = new FileStream(@"c:\orange\bancodadosf.bin", FileMode.Create);
            else
                streamListar = new FileStream(@"c:\orange\bancodadosf.bin", FileMode.Open);
            int i = 1; // contador local de registros
            BinaryReader leitor = new BinaryReader(streamListar); // instanciando o leitor de arquivo binário
           
            while (leitor.PeekChar() > -1)
            {
                fornecedor[i].cnpj = leitor.ReadDouble();
                fornecedor[i].email = leitor.ReadString();
                fornecedor[i].end = leitor.ReadString();
                fornecedor[i].idforn = leitor.ReadInt32();
                fornecedor[i].ie = leitor.ReadDouble();
                fornecedor[i].nomefantasia = leitor.ReadString();
                fornecedor[i].razaosocial = leitor.ReadString();
                fornecedor[i].tel = leitor.ReadInt32();
                i++;
            }
            leitor.Close(); //fecha o arquivo
            return i;
        }
        static int CarregarDadosC(Cliente[] cliente)
        {
            FileStream streamListar;
            if (!File.Exists(@"c:\orange\bancodadosc.bin")) // se o arquivo não existir, cria o arquivo
                streamListar = new FileStream(@"c:\orange\bancodadosc.bin", FileMode.Create);
            else
                streamListar = new FileStream(@"c:\orange\bancodadosc.bin", FileMode.Open);
            int i = 1; // contador local de registros
            BinaryReader leitor = new BinaryReader(streamListar); // instanciando o leitor de arquivo binário
            while (leitor.PeekChar() > -1)
            {
                cliente[i].cpf = leitor.ReadDouble();
                cliente[i].email = leitor.ReadString();
                cliente[i].end = leitor.ReadString();
                cliente[i].idcli = leitor.ReadInt32();
                cliente[i].nome = leitor.ReadString();
                cliente[i].rg = leitor.ReadDouble();
                cliente[i].tel = leitor.ReadInt32();
                i++;
            }
            leitor.Close(); //fecha o arquivo
            return i;
        }
        static int CarregarDadosP(Produto[]produto)
        {
            FileStream streamListar;
            if (!File.Exists(@"c:\orange\bancodadosp.bin")) // se o arquivo não existir, cria o arquivo
                streamListar = new FileStream(@"c:\orange\bancodadosp.bin", FileMode.Create);
            else
                streamListar = new FileStream(@"c:\orange\bancodadosp.bin", FileMode.Open);
            int i = 1; // contador local de registros
            BinaryReader leitor = new BinaryReader(streamListar); // instanciando o leitor de arquivo binário
            while (leitor.PeekChar() > -1)
            {
                produto[i].categoria = leitor.ReadString();
                produto[i].custo_compra = leitor.ReadDouble();
                produto[i].custo_medio_compra = leitor.ReadDouble();
                produto[i].custo_venda = leitor.ReadDouble();
                produto[i].idprod = leitor.ReadInt32();
                produto[i].nome = leitor.ReadString();
                produto[i].qtdest = leitor.ReadDouble();
                produto[i].qtdin = leitor.ReadDouble();
                produto[i].qtdout = leitor.ReadDouble();
                produto[i].qtdvend = leitor.ReadDouble();
                i++;
            }
            leitor.Close(); //fecha o arquivo
            return i;
        }
        static void SalvarDadosF(Fornecedor[]fornecedor)
        {
            int i = 1;
            FileStream streamGravar = new FileStream(@"c:\orange\bancodadosf.bin", FileMode.Create);
            BinaryWriter gravador = new BinaryWriter(streamGravar);
            while ((fornecedor[i].nomefantasia != null) && (i < 50))
            {
                gravador.Write(fornecedor[i].cnpj);
                gravador.Write(fornecedor[i].email);
                gravador.Write(fornecedor[i].end);
                gravador.Write(fornecedor[i].idforn);
                gravador.Write(fornecedor[i].ie);
                gravador.Write(fornecedor[i].nomefantasia);
                gravador.Write(fornecedor[i].razaosocial);
                gravador.Write(fornecedor[i].tel);
                gravador.Flush();// será que é um para cada registro?
                i++;
            }
            
            gravador.Close();
        }
        static void SalvarDadosC(Cliente[]cliente)
        {
            int i = 1;
            FileStream streamGravar = new FileStream(@"c:\orange\bancodadosc.bin", FileMode.Create);
            BinaryWriter gravador = new BinaryWriter(streamGravar);
            while ((cliente[i].nome!= null) && (i < 50))
            {
                gravador.Write(cliente[i].cpf);
                gravador.Write(cliente[i].email);
                gravador.Write(cliente[i].end);
                gravador.Write(cliente[i].idcli);
                gravador.Write(cliente[i].nome);
                gravador.Write(cliente[i].rg);
                gravador.Write(cliente[i].tel);
                gravador.Flush(); // será que é um para cada registro?
                i++;
            }
            gravador.Close();
        }
        static void SalvarDadosP(Produto[]produto)
        {
            int i = 1;
            FileStream streamGravar = new FileStream(@"c:\orange\bancodadosp.bin", FileMode.Create);
            BinaryWriter gravador = new BinaryWriter(streamGravar);
            while ((produto[i].nome != null) && (i < 50))
            {
                gravador.Write(produto[i].categoria);
                gravador.Write(produto[i].custo_compra);
                gravador.Write(produto[i].custo_medio_compra);
                gravador.Write(produto[i].custo_venda);
                gravador.Write(produto[i].idprod);
                gravador.Write(produto[i].nome);
                gravador.Write(produto[i].qtdest);
                gravador.Write(produto[i].qtdin);
                gravador.Write(produto[i].qtdout);
                gravador.Write(produto[i].qtdvend);
                gravador.Flush(); // será que é um para cada registro?
                i++;
            }
            gravador.Close();
        }
    }
}