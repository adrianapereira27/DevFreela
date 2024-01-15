using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core 1", "Minha Descrição de Projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core 2", "Minha Descrição de Projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core 3", "Minha Descrição de Projeto 3", 1, 1, 30000)
            };
            Users = new List<User>
            {
                new User("Adriana Pereira", "adriana@gmail.com", new DateTime(1982, 10, 27)),
                new User("Rodrigo Mora", "rodrigo@gmail.com", new DateTime(1979, 08, 08)),
                new User("Amanda Alejandra", "amanda@gmail.com", new DateTime(2002, 02, 24))
            };
            Skills = new List<Skill>
            {
                new Skill(".NET Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
