using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskApplication.Data.DTOs;

public class CreateTodoDto
{
    [JsonProperty("description")]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}