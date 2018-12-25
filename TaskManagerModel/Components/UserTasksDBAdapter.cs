using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;

namespace TaskManagerModel.Components
{
    internal class UserTasksDbAdapter : IContext
    {
        private readonly UserTasksDB _contex;

        private bool _disposed;

        public UserTasksDbAdapter()
        {
            _contex = new UserTasksDB();
            _disposed = false;
        }

        public int Insert(UserTask task)
        {
            return _contex.Insert(task);
        }

        public int Update(UserTask task)
        {
            return _contex.Update(task);
        }

        public int Delete(UserTask task)
        {
            return _contex.Delete(task);
        }

        public int DeleteById(long id)
        {
            return _contex.UserTasks.Where(x => x.Id == id).Delete();
        }

        public int DeleteByIds(List<long> tasksIdList)
        {
            return _contex.UserTasks.Where(x => tasksIdList.Contains(x.Id)).Delete();
        }

        public int DeleteAll()
        {
            return _contex.UserTasks.Delete();
        }

        public int DeleteCompleted(DateTime today)
        {
            return _contex.UserTasks.Where(t => t.TaskDate.Date < today.Date).Delete();
        }

        public IQueryable<UserTask> GetUserTasksTable()
        {
            return _contex.UserTasks;
        }

        public void Dispose()
        {
            _contex?.Dispose();
            _disposed = true;
        }

        ~UserTasksDbAdapter()
        {
            if (!_disposed)
                Dispose();
        }
    }
}
