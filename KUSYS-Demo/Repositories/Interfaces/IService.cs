using KUSYS_Demo.Models.DTO;

namespace KUSYS_Demo.Repositories.Interfaces
{
    public interface IService<TEntity>
    {



        Task<TEntity> GetById(string id);
        // Task Add(TEntity entity);
        Task Update(TEntity dbEntity, TEntity entity);
        Task Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllStudents();

        Task<List<CoursesStudents>> GetSelectedCourseByUserID(string username);

    }
}
