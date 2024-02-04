using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // comentado, porque será usado no MediatR
        /*public int Create(NewUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }*/

        /*public UserDetailsViewModel GetById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(p => p.Id == id);

            if (user == null)
            {
                return null;
            }

            return new UserDetailsViewModel(user.FullName, user.Email);
        }*/

    }
}
