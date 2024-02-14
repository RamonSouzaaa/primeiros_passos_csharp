using System;
using System.Collections.Generic;

//Arquivo de estudo POO
namespace Escola
{
    class Pessoa {
        private int id;
        private string nome;
        
        public int Id 
        { 
            get => id;
            set => id = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        public Pessoa() { }
        public Pessoa(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }

        override
        public string ToString()
        {
            return $"Id: {this.id}\nNome: {this.nome}";
        }
    }

    class Aluno : Pessoa
    {
        private List<Nota> notas;
        private List<Materia> materias;

        public Aluno() { }
        public Aluno(
            int id,
            string nome
        ) : base(id, nome)
        {
            this.notas = new List<Nota>();
            this.materias = new List<Materia>();
        }

        public string mostrarMedia(Materia materia)
        {
            return "Média " + materia.nome + ": " + this.calcularMedia(materia); 
        }
        
        public string mostrarMedias()
        {
            string medias = "";
            if (this.materias.Count == 0)
                return "Nenhuma matéria vínculada ao aluno!";
            
            foreach(Materia materia in this.materias)
            {
                medias += this.mostrarMedia(materia) + "\n";
            }

            return medias;
        }

        public void adicionarNota(Nota nota)
        {
            this.notas.Add(nota);
            this.adicionarMateria(nota.professor.materia);
        }

        private void adicionarMateria(Materia materia)
        {
            if (this.materias.Contains(materia))
                return;

            this.materias.Add(materia);
        }
        
        private float calcularMedia(Materia materia)
        {
            float media = 0;
            int quantidadeNotas = 0;

            if (this.notas.Count == 0)
                return 0;

            foreach (Nota item in this.notas)
            {
                if (item.professor.materia.nome == materia.nome)
                {
                    quantidadeNotas++;
                    media += item.Valor;
                }
            }

            return media / quantidadeNotas;
        }
    }

    internal class Nota
    {
        private float valor;
        public Professor professor { get; private set; }

        public float Valor 
        {
            get => valor;
            set => valor = value;
        }

        public Nota() { }
        public Nota(float valor, Professor professor)
        {
            this.valor = valor;
            this.professor = professor;
        }
    }

    class Materia {
        public string nome;

        public Materia() { }
        public Materia(string nome)
        {
            this.nome = nome;
        }
    }

    class Professor : Pessoa
    {
        public Materia materia { get; private set; }

        public Professor() { }
        public Professor(int id, string nome, Materia materia) : base(id, nome)
        {
            this.materia = materia;
        }
    }

    class Controlador
    {
        private List<Professor> professores;
        private List<Aluno> alunos;
        private List<Materia> materias;
    }

    class Test 
    {
        static void Main(string[] args)
        {
            Materia matematica = new Materia("Matemática");
            Materia portugues = new Materia("Português");
            Materia ingles = new Materia("Inglês");

            Professor joaoProfessor = new Professor(1, "João", matematica);
            Professor claudioProfessor = new Professor(2, "Claudio", portugues);
            Professor joanaProfessor = new Professor(3, "Joana", ingles);

            Aluno joaoAluno = new Aluno(1, "João");
            Aluno mariaAluno = new Aluno(2, "Maria");

            joaoAluno.adicionarNota(new Nota(8.2f, joaoProfessor));
            joaoAluno.adicionarNota(new Nota(3.2f, joaoProfessor));
            joaoAluno.adicionarNota(new Nota(5.1f, joaoProfessor));

            joaoAluno.adicionarNota(new Nota(9.3f, claudioProfessor));
            joaoAluno.adicionarNota(new Nota(6.6f, claudioProfessor));

            joaoAluno.adicionarNota(new Nota(2.3f, joanaProfessor));
            joaoAluno.adicionarNota(new Nota(4.8f, joanaProfessor));
            joaoAluno.adicionarNota(new Nota(8.2f, joanaProfessor));

            mariaAluno.adicionarNota(new Nota(7.2f, joaoProfessor));
            mariaAluno.adicionarNota(new Nota(6.6f, joaoProfessor));
            mariaAluno.adicionarNota(new Nota(4.7f, joaoProfessor));

            Console.WriteLine(joaoAluno.ToString());
            Console.WriteLine(joaoAluno.mostrarMedias());
            Console.WriteLine("----------------------------------");
            Console.WriteLine(mariaAluno.ToString());
            Console.WriteLine(mariaAluno.mostrarMedias());
            Console.WriteLine("----------------------------------");
            Console.WriteLine(joaoProfessor.ToString());

        }
    }
}

