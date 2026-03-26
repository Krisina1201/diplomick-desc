using System;
using System.Collections.Generic;

namespace Diplom.Models;

/// <summary>
/// Инвентарь в аудиториях
/// </summary>
public partial class Inventory
{
    /// <summary>
    /// Уникальный идентификатор инвентаря
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID аудитории, где находится инвентарь
    /// </summary>
    public int ClassroomId { get; set; }

    /// <summary>
    /// Наименование предмета
    /// </summary>
    public string ItemName { get; set; } = null!;

    /// <summary>
    /// Тип инвентаря
    /// </summary>
    public int ItemType { get; set; }

    /// <summary>
    /// Производитель
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Модель
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Инвентарный номер
    /// </summary>
    public int? InventoryNumber { get; set; }

    /// <summary>
    /// Описание состояния
    /// </summary>
    public string? ConditionDescription { get; set; }

    /// <summary>
    /// Дата покупки
    /// </summary>
    public DateOnly? PurchaseDate { get; set; }

    /// <summary>
    /// Гарантия до
    /// </summary>
    public DateOnly? WarrantyUntil { get; set; }

    /// <summary>
    /// Примечания
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления записи
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual InventoryType ItemTypeNavigation { get; set; } = null!;
}
