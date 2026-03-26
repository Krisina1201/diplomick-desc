using System;
using System.Collections.Generic;

namespace Diplom.Models;

/// <summary>
/// Аудитории
/// </summary>
public partial class Classroom
{
    /// <summary>
    /// Уникальный идентификатор аудитории
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер аудитории
    /// </summary>
    public string RoomNumber { get; set; } = null!;

    /// <summary>
    /// Название аудитории
    /// </summary>
    public string RoomName { get; set; } = null!;

    /// <summary>
    /// Описание аудитории
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Вместимость
    /// </summary>
    public int? Capacity { get; set; }

    /// <summary>
    /// ID ответственного лица
    /// </summary>
    public int? ResponsibleId { get; set; }

    /// <summary>
    /// Данные QR-кода
    /// </summary>
    public string? QrCodeData { get; set; }

    /// <summary>
    /// Активна ли аудитория
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления записи
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

  

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ResponsiblePerson? Responsible { get; set; }

    public string StatusText => IsActive == true ? "Активна" : "Неактивна";
}
