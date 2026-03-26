using System;
using System.Collections.Generic;

namespace Diplom.Models;

/// <summary>
/// Ответственные лица
/// </summary>
public partial class ResponsiblePerson
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Position { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();

    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

}
