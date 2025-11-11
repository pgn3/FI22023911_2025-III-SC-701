using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksConsole.Models;

public class TitleTag
{
    [Key]
    public int TitleTagId { get; set; }

    [Required]
    [ForeignKey(nameof(Title))]
    public int TitleId { get; set; }

    [Required]
    [ForeignKey(nameof(Tag))]
    public int TagId { get; set; }

    public Title? Title { get; set; }
    public Tag? Tag { get; set; }
}
