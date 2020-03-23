using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reminder.Storage.Core;

namespace Reminder.Storage.SqlServer.EF.Context
{
	public class ReminderStorageContext : DbContext
	{
		public DbSet<ReminderItemDto> ReminderItems { get; set; }

		public ReminderStorageContext(DbContextOptions<ReminderStorageContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ReminderItemDto>(entity =>
			{
				entity
					.ToTable("ReminderItem")
					.HasIndex(e => e.Status);

				entity.Property(e => e.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

				entity.Property(e => e.ContactId)
					.IsRequired()
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.TargetDate)
					.IsRequired()
					.HasColumnType("datetimeoffset(7)");

				entity.Property(e => e.Message)
					.IsRequired()
					.HasMaxLength(200)
					.IsUnicode(true);

				entity.Property(e => e.Status)
					.HasColumnName("StatusId")
					.IsRequired()
					.HasConversion(
						//new EnumToStringConverter<ReminderItemStatus>());
						new EnumToNumberConverter<ReminderItemStatus, int>());

				entity.Property(e => e.CreatedDate)
					.HasColumnType("datetimeoffset(7)")
					.HasDefaultValueSql("sysdatetimeoffset()")
					.ValueGeneratedOnAdd();

				entity.Property(e => e.UpdatedDate)
					.HasColumnType("datetimeoffset(7)")
					.HasDefaultValueSql("sysdatetimeoffset()")
					.ValueGeneratedOnAddOrUpdate();
			});
		}
	}
}
