using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Models;

public partial class FankyPopContext : DbContext
{
    public FankyPopContext()
    {
    }

    public FankyPopContext(DbContextOptions<FankyPopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryType> InventoryTypes { get; set; }

    public virtual DbSet<ResponsiblePerson> ResponsiblePersons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FankyPop;Username=admin;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("classrooms_pkey");

            entity.ToTable("classrooms", tb => tb.HasComment("Аудитории"));

            entity.HasIndex(e => e.QrCodeData, "classrooms_qr_code_data_key").IsUnique();

            entity.HasIndex(e => e.RoomNumber, "classrooms_room_number_key").IsUnique();

            entity.HasIndex(e => e.IsActive, "idx_classrooms_is_active");

            entity.HasIndex(e => e.ResponsibleId, "idx_classrooms_responsible_id");

            entity.Property(e => e.Id)
                .HasComment("Уникальный идентификатор аудитории")
                .HasColumnName("id");
            entity.Property(e => e.Capacity)
                .HasComment("Вместимость")
                .HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Дата создания записи")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasComment("Описание аудитории")
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasComment("Активна ли аудитория")
                .HasColumnName("is_active");
            entity.Property(e => e.QrCodeData)
                .HasMaxLength(50)
                .HasComment("Данные QR-кода")
                .HasColumnName("qr_code_data");
            entity.Property(e => e.ResponsibleId)
                .HasComment("ID ответственного лица")
                .HasColumnName("responsible_id");
            entity.Property(e => e.RoomName)
                .HasMaxLength(100)
                .HasComment("Название аудитории")
                .HasColumnName("room_name");
            entity.Property(e => e.RoomNumber)
                .HasMaxLength(10)
                .HasComment("Номер аудитории")
                .HasColumnName("room_number");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Дата обновления записи")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Responsible).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.ResponsibleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("classrooms_responsible_id_fkey");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inventory_pkey");

            entity.ToTable("inventory", tb => tb.HasComment("Инвентарь в аудиториях"));

            entity.HasIndex(e => e.ClassroomId, "idx_inventory_classroom_id");

            entity.HasIndex(e => e.ItemType, "idx_inventory_item_type");

            entity.HasIndex(e => e.PurchaseDate, "idx_inventory_purchase_date");

            entity.HasIndex(e => e.InventoryNumber, "inventory_inventory_number_key").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("Уникальный идентификатор инвентаря")
                .HasColumnName("id");
            entity.Property(e => e.ClassroomId)
                .HasComment("ID аудитории, где находится инвентарь")
                .HasColumnName("classroom_id");
            entity.Property(e => e.ConditionDescription)
                .HasComment("Описание состояния")
                .HasColumnName("condition_description");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Дата создания записи")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.InventoryNumber)
                .HasComment("Инвентарный номер")
                .HasColumnName("inventory_number");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .HasComment("Наименование предмета")
                .HasColumnName("item_name");
            entity.Property(e => e.ItemType)
                .HasComment("Тип инвентаря")
                .HasColumnName("item_type");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .HasComment("Производитель")
                .HasColumnName("manufacturer");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasComment("Модель")
                .HasColumnName("model");
            entity.Property(e => e.Notes)
                .HasComment("Примечания")
                .HasColumnName("notes");
            entity.Property(e => e.PurchaseDate)
                .HasComment("Дата покупки")
                .HasColumnName("purchase_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Дата обновления записи")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WarrantyUntil)
                .HasComment("Гарантия до")
                .HasColumnName("warranty_until");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventory_classroom_id_fkey");

            entity.HasOne(d => d.ItemTypeNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ItemType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventory_item_type_fk");
        });

        modelBuilder.Entity<InventoryType>(entity =>
        {
            entity.HasKey(e => e.InventoryTypeId).HasName("inventory_type_pkey");

            entity.ToTable("inventory_type", tb => tb.HasComment("Типы инвентаря"));

            entity.Property(e => e.InventoryTypeId).HasColumnName("inventory_type_id");
            entity.Property(e => e.InventoryTypeTitle)
                .HasMaxLength(255)
                .HasColumnName("inventory_type_title");
        });

        modelBuilder.Entity<ResponsiblePerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("responsible_persons_pkey");

            entity.ToTable("responsible_persons", tb => tb.HasComment("Ответственные лица"));

            entity.HasIndex(e => new { e.LastName, e.FirstName, e.MiddleName }, "idx_responsible_persons_full_name");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasColumnName("position");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
