using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TaskApplication.Data.Models;

[Table("Todos")]
public class Todo
{
    [Key]
    [JsonProperty("id")]
    [Column("id")]
    public Guid Id { get; set; }
    
    [JsonProperty("description")]
    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [JsonProperty("is_important")]
    [Column("is_important")]
    public bool IsImportant { get; set; }
    
    [JsonProperty("is_completed")]
    [Column("is_completed")]
    public bool IsCompleted { get; set; }
    
    [JsonProperty("created_at")]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}