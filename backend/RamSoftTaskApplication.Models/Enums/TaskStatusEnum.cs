using System.Runtime.Serialization;

namespace RamSoftTaskApplication.Models.Enums;

public enum TaskStatusEnum
{
    [EnumMember(Value = "To Do")]
    ToDo,
    [EnumMember(Value = "In Progress")]
    InProgress,
    Done
}