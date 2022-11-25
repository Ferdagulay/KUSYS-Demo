using KUSYS_Demo.Models.DTO;

namespace KUSYS_Demo.Repositories.Interfaces
{
    public interface IService<TEntity>
    {



        Task<TEntity> GetById(string id);

        // Task Add(TEntity entity); It can be added here. But I choosed different way to add new records.
        Task Update(TEntity dbEntity, TEntity entity);
        Task Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<List<CoursesStudents>> GetSelectedCourseByUserID(string username);

    }
}
