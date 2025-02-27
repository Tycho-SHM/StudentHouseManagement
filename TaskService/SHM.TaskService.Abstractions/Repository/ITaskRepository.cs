using SHM.TaskService.Model;

namespace SHM.TaskService.Abstractions.Repository;

public interface ITaskRepository
{
    public Task<IEnumerable<SHMTask>> GetALl();
    public Task<SHMTask> GetById(Guid id);
    public Task<SHMTask> Create(SHMTask task);
}