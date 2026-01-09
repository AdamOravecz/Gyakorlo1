using System;
using System.Collections.Generic;

namespace Oravecz_Adam_BackEnd.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public DateTime? PublishDate { get; set; }

    public int? AuthorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Category? Category { get; set; }
}
