using System;


namespace appSerie
{
    class Program
    {
        static serieRepositorio repositorio = new serieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();
            while(opcaoUsuario != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSerie();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Programa Finalizado!");
            Console.ReadKey();
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da Série: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite o ID da Série: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            repositorio.Exclui(indiceSerie);
        }

        private static void ListarSerie(){
            Console.WriteLine("Listar séries");
            var lista = repositorio.Lista();
            if(lista.Count == 0){
                Console.WriteLine("Nenhuma Série cadastrada.");
                return;
            }
            foreach(var serie in lista){
                var excluido = serie.retornaExlcuido();
                Console.WriteLine("#ID {0}: = {1} {2}", serie.retornaId(), serie.retornaTitulo(), excluido ? "Excluido" : "");
                //Console.WriteLine($"ID{serie.retornaId()}: {serie.retornaTitulo()} - {0}", excluido ? "Excluido" :"");
            }
        }
        private static void InserirSerie(){
            int total = 0;
            int entradaGenero;
            foreach(int i in Enum.GetValues(typeof(Genero))){
                Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero),i)}");
                total += 1;
            }
            Console.Write("Escolha o genero dentre as opção acima:");
            while (!(int.TryParse(Console.ReadLine(), out entradaGenero) && (entradaGenero >= 0 && entradaGenero <= total)))
                {
                    Console.Write("Opção inválida!!Escolha o Genero dentre as opções acima: ");
                }
            Console.Write("Digite o Titulo: ");
            string entradaTitulo = Console.ReadLine();
            Console.Write("Digite o Ano de lançamento: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.Write("Digite a Descrição da Série: ");
            string entradaDescicao = Console.ReadLine();

            serie novaSerie = new serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescicao);
            repositorio.Insere(novaSerie);
        }
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Lista Series");
            Console.WriteLine("2 - Inserir nova Serie");
            Console.WriteLine("3 - Atualizar Serie");
            Console.WriteLine("4 - Excluir Serie");
            Console.WriteLine("5 - Visualizar Serie");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
        private static void AtualizarSerie(){
            Console.Write("Digite o id da Série: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} ---- {1}", i, Enum.GetName(typeof(Genero),i));
            }
            Console.Write("Digite o genero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.Write("Digite o Titulo da Série: ");
            string entradaTitulo = Console.ReadLine();
            Console.Write("Digite o Ano de Lançamento: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.Write("Digite a descrição da Série: ");
            string entradaDescicao = Console.ReadLine();
            serie atualizarSerie = new serie(
                id: indiceSerie,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano : entradaAno,
                descricao: entradaDescicao);
            repositorio.Atualiza(indiceSerie,atualizarSerie);          
        }
    }
}
