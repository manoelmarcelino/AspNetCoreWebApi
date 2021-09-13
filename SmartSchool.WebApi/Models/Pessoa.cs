namespace SmartSchool.WebApi.Models
{
    public abstract class Pessoa
    {
        protected Pessoa() { }
        protected Pessoa(int id, string nome, string sobrenome, string telefone)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
        }

        public int Id { get; set; }   
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
    }
}   
