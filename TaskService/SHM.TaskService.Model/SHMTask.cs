namespace SHM.TaskService.Model;

public class SHMTask
{
    public Guid Id { get; set; }
    public string Task { get; set; }
    public DateTime CreatedDateTime { get; set; }
}