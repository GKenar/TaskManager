//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/t4models).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace TaskManagerModel
{
	/// <summary>
	/// Database       : UserTasks
	/// Data Source    : UserTasks
	/// Server Version : 3.14.2
	/// </summary>
	public partial class UserTasksDB : DataConnection
	{
		public ITable<UserTask> UserTasks { get { return this.GetTable<UserTask>(); } }

		public UserTasksDB()
		{
			InitDataContext();
		}

		public UserTasksDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
		}

		partial void InitDataContext();
	}

	[Table("UserTasks")]
	public partial class UserTask
	{
		[Column("id"),          PrimaryKey,  Identity] public long     Id          { get; set; } // integer
		[Column("name"),        NotNull              ] public string   Name        { get; set; } // text(max)
		[Column("description"),    Nullable          ] public string   Description { get; set; } // text(max)
		[Column("priority"),    NotNull              ] public long     Priority    { get; set; } // integer
		[Column("taskDate"),    NotNull              ] public DateTime TaskDate    { get; set; } // text(max)
		[Column("notifyDate"),  NotNull              ] public DateTime NotifyDate  { get; set; } // text(max)
		[Column("isNotified"),  NotNull              ] public bool     IsNotified  { get; set; } // integer
	}

	public static partial class TableExtensions
	{
		public static UserTask Find(this ITable<UserTask> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}
	}
}
