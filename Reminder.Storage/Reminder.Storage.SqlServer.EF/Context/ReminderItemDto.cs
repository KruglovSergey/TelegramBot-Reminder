using System;
using Reminder.Storage.Core;

namespace Reminder.Storage.SqlServer.EF.Context
{
	public class ReminderItemDto
	{
		public Guid Id { get; set; }

		public string ContactId { get; set; }

		public DateTimeOffset TargetDate { get; set; }

		public string Message { get; set; }

		public ReminderItemStatus Status { get; set; }

		public DateTimeOffset CreatedDate { get; set; }

		public DateTimeOffset UpdatedDate { get; set; }

		public ReminderItemDto()
		{

		}

		public ReminderItemDto(ReminderItemRestricted restricted)
		{
			ContactId = restricted.ContactId;
			TargetDate = restricted.Date;
			Message = restricted.Message;
			Status = restricted.Status;
		}

		public ReminderItem ToReminderItem()
		{
			return new ReminderItem
			{
				Id = Id,
				ContactId = ContactId,
				Date = TargetDate,
				Message = Message,
				Status = Status
			};
		}
	}
}
