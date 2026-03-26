using System;
using System.Collections.Generic;

namespace Diplom.Models;

/// <summary>
/// Типы инвентаря
/// </summary>
public partial class InventoryType
{
    public int InventoryTypeId { get; set; }

    public string InventoryTypeTitle { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
