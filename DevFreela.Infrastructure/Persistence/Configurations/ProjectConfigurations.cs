using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder         // relacionamento do projeto com o freelancer
                .HasOne(p => p.Freelancer)
                .WithMany(f => f.FreelanceProjects)  // tem muitos projetos que o freelancer trabalhou
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict); // não permite deletar uma entidade que tenha relacionamento com outra

            builder         // relacionamento do projeto com o cliente
                .HasOne(p => p.Client)
                .WithMany(f => f.OwnedProjects)   // tem muitos projetos que o cliente é dono
                .HasForeignKey(p => p.IdClient)
                .OnDelete(DeleteBehavior.Restrict); // não permite deletar uma entidade que tenha relacionamento com outra

        }
    }
}
