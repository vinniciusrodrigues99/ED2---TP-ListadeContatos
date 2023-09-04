using System;
using System.Collections.Generic;
//Vinnicius O. Rodrigues
namespace ContatosApp
{
    class Data
    {
        public int dia;
        public int mes;
        public int ano;

        public Data(int dia, int mes, int ano)
        {
            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public override string ToString()
        {
            return $"{dia:D2}/{mes:D2}/{ano:D4}";
        }
    }

    class Telefone
    {
        public string tipo;
        public string numero;
        public bool principal;

        public Telefone(string tipo, string numero, bool principal)
        {
            this.tipo = tipo;
            this.numero = numero;
            this.principal = principal;
        }
    }

    class Contato
    {
        public string email;
        private string nome;
        private Data dtNasc;
        private List<Telefone> telefones = new List<Telefone>();

        public Contato(string email, string nome, Data dtNasc)
        {
            this.email = email;
            this.nome = nome;
            this.dtNasc = dtNasc;
        }

        public int getIdade()
        {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dtNasc.ano;
            if (hoje.Month < dtNasc.mes || (hoje.Month == dtNasc.mes && hoje.Day < dtNasc.dia))
                idade--;
            return idade;
        }

        public void adicionarTelefone(Telefone t)
        {
            telefones.Add(t);
        }

        public string getTelefonePrincipal()
        { foreach (Telefone telefone in telefones) {
                if (telefone.principal)
                    return telefone.numero;
            }
            return "Nenhum telefone principal";
        }

        public override string ToString()
        {
            string telefonePrincipal = getTelefonePrincipal();
            return $"Nome: {nome}\nEmail: {email}\nData de Nascimento: {dtNasc}\nTelefone Principal: {telefonePrincipal}";
        }

        public override bool Equals(object obj)
        {  if (obj == null || GetType() != obj.GetType())
                return false;
            Contato other = (Contato)obj;
            return email == other.email;
        }
    }
    class Contatos
    {
        private List<Contato> agenda = new List<Contato>();

        public bool adicionar(Contato c)
        {if (!agenda.Contains(c))
            {
                agenda.Add(c);
                return true;
            }
            return false;
        }

        public Contato pesquisar(string email)
        {
            return agenda.Find(c => c.email == email);
        }

        public bool alterar(Contato contatoModificado)
        { int index = agenda.FindIndex(item => item.email == contatoModificado.email);
            if (index != -1)
            {
                agenda[index] = contatoModificado;
                return true;
            }
            return false;
        }

        public bool remover(Contato c)
        { return agenda.Remove(c);
        }

        public void listar()
        {
            foreach (Contato c in agenda)
            {
                Console.WriteLine(c);
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Contatos agenda = new Contatos();

            while (true)
            {
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar contato");
                Console.WriteLine("2. Adicionar telefone no contato");
                Console.WriteLine("3. Pesquisar contato");
                Console.WriteLine("4. Alterar contato");
                Console.WriteLine("5. Remover contato");
                Console.WriteLine("6. Listar contatos");

                int opcao = Convert.ToInt32(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo...");
                        return;
                    case 1:
                        Console.Write("Nome do contato: ");
                        string nome = Console.ReadLine();
                        Console.Write("Email do contato: ");
                        string email = Console.ReadLine();
                        Console.Write("Dia de nascimento: ");
                        int dia = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Mês de nascimento: ");
                        int mes = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Ano de nascimento: ");
                        int ano = Convert.ToInt32(Console.ReadLine());

                        Data dataNasc = new Data(dia, mes, ano);
                        Contato novoContato = new Contato(email, nome, dataNasc);
                        agenda.adicionar(novoContato);
                        Console.WriteLine("Contato adicionado com sucesso!");
                        break;

                    case 2:
                        Console.Write("Email do contato: ");
                        string emailPesquisa = Console.ReadLine();
                        Contato contatoEncontrado = agenda.pesquisar(emailPesquisa);
                        if (contatoEncontrado != null)
                        {
                            Console.Write("Tipo do telefone: ");
                            string tipoTelefone = Console.ReadLine();
                            Console.Write("Número do telefone: ");
                            string numeroTelefone = Console.ReadLine();
                            Console.Write("Telefone principal (S/N): ");
                            bool principal = Console.ReadLine().ToUpper() == "S";

                            Telefone novoTelefone = new Telefone(tipoTelefone, numeroTelefone, principal);
                            contatoEncontrado.adicionarTelefone(novoTelefone);
                            Console.WriteLine("Telefone adicionado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado.");
                        }
                        break;

                    case 3:
                        Console.Write("Email do contato: ");
                        string emailPesquisa2 = Console.ReadLine();
                        Contato contatoPesquisado = agenda.pesquisar(emailPesquisa2);
                        if (contatoPesquisado != null)
                        {
                            Console.WriteLine(contatoPesquisado);
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado.");
                        }
                        break;

                    case 4:
                        Console.Write("Email do contato: ");
                        string emailPesquisa3 = Console.ReadLine();
                        Contato contatoEncontrado2 = agenda.pesquisar(emailPesquisa3);
                        if (contatoEncontrado2 != null)
                        {
                            Console.Write("Novo nome do contato: ");
                            string novoNome = Console.ReadLine();
                            Console.Write("Nova data de nascimento:\nDia: ");
                            int novoDia = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Mês: ");
                            int novoMes = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Ano: ");
                            int novoAno = Convert.ToInt32(Console.ReadLine());

                            Data novaDataNasc = new Data(novoDia, novoMes, novoAno);
                            Contato contatoModificado = new Contato(contatoEncontrado2.email, novoNome, novaDataNasc);
                            if (agenda.alterar(contatoModificado))
                            {
                                Console.WriteLine("Contato alterado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Falha ao alterar contato.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado.");
                        }
                        break;

                    case 5:
                        Console.Write("Email do contato: ");
                        string emailPesquisa4 = Console.ReadLine();
                        Contato contatoRemover = agenda.pesquisar(emailPesquisa4);
                        if (contatoRemover != null)
                        {
                            agenda.remover(contatoRemover);
                            Console.WriteLine("Contato removido com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado.");
                        }
                        break;

                    case 6:
                        agenda.listar();
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
