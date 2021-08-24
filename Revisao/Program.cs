using System;


namespace Revisao
{
    class Program
    {
        static void Main(string[] args)
        {
            Aluno[] alunos = new Aluno[5];
            var indiceAluno = 0;
            string opcaoUsuario = ObterOpcaoUsuario();
            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        Console.WriteLine("Informe o nome do aluno: ");
                        Aluno aluno = new Aluno();
                        aluno.Nome = Console.ReadLine();
                        Console.WriteLine("Informe a Nota do Aluno: ");
                        //aluno.Nota = Convert.ToDecimal(Console.ReadLine());
                        if(Decimal.TryParse(Console.ReadLine(), out decimal nota))
                        {
                            aluno.Nota = nota;
                        }
                        else
                        {
                            throw new ArgumentException("valor da nota deve ser decimal");
                        }
                        alunos[indiceAluno] = aluno;
                        indiceAluno++;
                        break;
                    case "2":
                        foreach(var a in alunos)
                        {
                            //if(!string.IsNullOrEmpty(a.Nome))
                            if(a.Nome != null)
                                Console.WriteLine($"ALuno: {a.Nome} - Nota: {a.Nota}");
                        }
                        break;
                    case "3":
                        decimal notaTotal = 0;
                        var nrAlunos = 0;
                        for(int i=0; i<alunos.Length; i++)
                        {
                            if(alunos[i].Nome != null)
                            {
                                notaTotal += alunos[i].Nota;
                                nrAlunos++;
                            }
                        }
                        var mediaGeral = notaTotal / nrAlunos;
                        Conceito conceitoGeral;
                        if(mediaGeral < 2){
                            conceitoGeral = Conceito.E;
                        }
                        else if(mediaGeral < 4){
                            conceitoGeral = Conceito.D;
                        }
                        else if(mediaGeral < 6){
                            conceitoGeral = Conceito.C;
                        }
                        else if(mediaGeral < 8){
                            conceitoGeral = Conceito.B;
                        }
                        else
                        {
                            conceitoGeral = Conceito.A;
                        }
                        Console.WriteLine($"Media Geral: {mediaGeral}, Conceito {conceitoGeral}");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static string ObterOpcaoUsuario()
        {
            string opcaoUsuario;
            Console.WriteLine("Selecione a Opção Desejada");
            Console.WriteLine("1 - Inserir novo aluno");
            Console.WriteLine("2 - Listar alunos");
            Console.WriteLine("3 - Calcular média geral");
            Console.WriteLine("x- Sair \n");
            Console.WriteLine();
            opcaoUsuario = Console.ReadLine();
            return opcaoUsuario;
        }
    }
}
