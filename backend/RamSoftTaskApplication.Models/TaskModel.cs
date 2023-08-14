using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RamSoftTaskApplication.Models.Enums;

namespace RamSoftTaskApplication.Models;

public class TaskModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsFavorite { get; set; }
    public byte[]? ImageData { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.ToDo;
}