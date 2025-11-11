using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksConsole.Models;

public class Title
{
    [Key]
    public int TitleId { get; set; }

    [Required]
    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }

    [Required]
    public string TitleName { get; set; } = string.Empty;

    public Author? Author { get; set; }

    public ICollection<TitleTag> TitleTags { get; set; } = new List<TitleTag>();
}
