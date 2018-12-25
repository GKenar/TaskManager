using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TaskManagerCommon.Components;
using TaskManagerModel;
using TaskManagerModel.Components;

namespace TaskManagerUnitTest
{
    [TestFixture]
    public class TasksFilterTests
    {
        private List<UserTask> GenerateUserTaskList()
        {
            return new List<UserTask>
            {
                new UserTask { Priority = (long)TaskPriority.Undefined },
                new UserTask { Priority = (long)TaskPriority.High },
                new UserTask { Priority = (long)TaskPriority.Medium },
                new UserTask { Priority = (long)TaskPriority.Low }
            };
        }

        [TestCase(TaskPriority.High, FilterType.HighPriority)]
        [TestCase(TaskPriority.Medium, FilterType.MediumPriority)]
        [TestCase(TaskPriority.Low, FilterType.LowPriority)]
        public void FilterPriority(TaskPriority priority, FilterType filterType)
        {
            var filter = new TasksFilter();
            var usertasks = GenerateUserTaskList();

            var result = filter.Filter(usertasks.AsQueryable(), filterType);

            Assert.IsTrue(result.All(t => t.Priority == (long)priority));
        }

        [Test]
        public void FilterPriorityAll()
        {
            var filter = new TasksFilter();
            var usertasks = GenerateUserTaskList();

            var result = filter.Filter(usertasks.AsQueryable(), FilterType.All);

            Assert.IsTrue(result.Count() == usertasks.Count());
        }

        [Test]
        public void FilterTypeUndefined()
        {
            var filter = new TasksFilter();
            var usertasks = GenerateUserTaskList();

            Assert.Throws<ArgumentException>(() => 
                filter.Filter(usertasks.AsQueryable(), FilterType.Undefined)
            );
        }

        [Test]
        public void FilterQueryNull()
        {
            var filter = new TasksFilter();

            Assert.Throws<ArgumentNullException>(() =>
                filter.Filter(null, FilterType.All)
            );
        }
    }
}
