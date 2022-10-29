using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskApplication.Data.DTOs;

public class UpdateTodoDto
{
    [JsonProperty("description")]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [JsonProperty("is_important")]
    public bool? IsImportant { get; set; }
    
    [JsonProperty("is_completed")]
    public bool? IsCompleted { get; set; }
}