using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Moq;
using NUnit.Framework;
using TaskManagerCommon.Components;
using TaskManagerModel;
using TaskManagerModel.Components;
using TaskManagerUnitTest.Fakes;

namespace TaskManagerUnitTest
{
    [TestFixture]
    public class DataModelTestsMock
    {
        [Test]
        public void InsertCorrectTask()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);

            var task = new UserTask
            {
                Name = "TestName",
                Description = "TestDescription",
                Priority = (long)TaskPriority.Medium,
                IsNotified = false
            };

            dataModel.AddTask(task);

            contextMock.Verify(c => c.Insert(task), Times.Once);
        }

        [TestCase("", "TaskDescription", TaskPriority.Medium, Description = "Empty name")]
        [TestCase(null, "TaskDescription", TaskPriority.Medium, Description = "Null name")]
        [TestCase("Task", "", TaskPriority.Medium, Description = "Empty description")]
        [TestCase("Task", null, TaskPriority.Medium, Description = "Null description")]
        [TestCase("Task", "TaskDescription", TaskPriority.Undefined, Description = "Priority undefined")]
        public void InsertInvalidTask(string name, string descr, TaskPriority prior)
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);

            var task = new UserTask
            {
                Name = name,
                Description = descr,
                Priority = (long)prior,
                IsNotified = false
            };

            Assert.Throws<ValidationException>(() => dataModel.AddTask(task));
        }

        [Test]
        public void InsertNullTask()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            Assert.Throws<ArgumentNullException>(() => dataModel.AddTask(null));
        }

        [Test]
        public void UpdateCorrectTask()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var task = new UserTask
            {
                Id = 1,
                Name = "TestName",
                Description = "TestDescription",
                Priority = (long)TaskPriority.Medium,
                IsNotified = false
            };

            dataModel.UpdateTask(task);

            contextMock.Verify(c => c.Update(task), Times.Once);
        }

        [TestCase(-1, "Task", "TaskDescription", TaskPriority.Medium, Description = "Invalid id < 0")]
        [TestCase(0, "Task", "TaskDescription", TaskPriority.Medium, Description = "Invalid id = 0")]
        [TestCase(1, "", "TaskDescription", TaskPriority.Medium, Description = "Empty name")]
        [TestCase(1, null, "TaskDescription", TaskPriority.Medium, Description = "Null name")]
        [TestCase(1, "Task", "", TaskPriority.Medium, Description = "Empty description")]
        [TestCase(1, "Task", null, TaskPriority.Medium, Description = "Null description")]
        [TestCase(1, "Task", "TaskDescription", TaskPriority.Undefined, Description = "Priority undefined")]
        public void UpdateInvalidTask(long id, string name, string descr, TaskPriority prior)
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var task = new UserTask
            {
                Id = id,
                Name = name,
                Description = descr,
                Priority = (long)prior,
                IsNotified = false
            };

            Assert.Throws<ValidationException>(() => dataModel.UpdateTask(task));
        }

        [Test]
        public void UpdateNullTask()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            Assert.Throws<ArgumentNullException>(() => dataModel.UpdateTask(null));
        }

        [Test]
        public void DeleteCorrectTask()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);

            var id = 1;

            dataModel.DeleteTask(id);

            contextMock.Verify(c => c.DeleteById(id), Times.Once);
        }

        [TestCase(-1, Description = "Invalid id = -1 < 0")]
        [TestCase(0, Description = "Invalid id = 0")]
        public void DeleteInvalidTask(long id)
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            Assert.Throws<ArgumentException>(() => dataModel.DeleteTask(id));
        }

        [Test]
        public void DeleteCorrectTasks()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var taskIds = new List<long> { 1, 2, 3 };

            dataModel.DeleteTasks(taskIds);

            contextMock.Verify(c => c.DeleteByIds(taskIds), Times.Once);
        }

        [Test]
        public void DeleteInvalidIdTasks()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var taskIds = new List<long> { 0, -1, -2, -3 };

            Assert.Throws<ArgumentException>(() => dataModel.DeleteTasks(taskIds));
        }

        [Test]
        public void DeleteCompletedTaskCorrect()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var today = new DateTime(2005, 1, 2);

            dataModel.DeleteCompletedTasks(today);

            contextMock.Verify(c => c.DeleteCompleted(today));
        }

        [Test]
        public void DeleteCompletedTaskInvalid()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var today = new DateTime(2005, 1, 2, 1, 2, 5);

            Assert.Throws<ValidationException>(() => dataModel.DeleteCompletedTasks(today));
        }

        [Test]
        public void DeleteNullListTasks()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            Assert.Throws<ArgumentNullException>(() => dataModel.DeleteTasks(null));
        }

        [Test]
        public void GetAllTasks()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var tasks = dataModel.GetAllTasks();

            Assert.AreEqual(GenerateUserTaskList().Count, tasks.Count);
        }

        [Test, TestCaseSource(typeof(DatesForTest), nameof(DatesForTest.TestCase))]
        public void GetTasksOfDay(DateTime date)
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var tasks = dataModel.GetTasksOfDay(date);

            Assert.AreEqual(date, tasks.Single().TaskDate);
        }

        [Test]
        public void GetTasksOfTwoDays()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var tasks = dataModel.GetTasksOfDays(new DateTime(1, 1, 1), new DateTime(1, 1, 2));

            Assert.True(tasks.Count == 2 && tasks[0].TaskDate.Day < 3 && tasks[1].TaskDate.Day < 3);
        }

        [Test]
        public void GetTasksOfTwoDaysRevert()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            Assert.Throws<ArgumentException>(
                () => dataModel.GetTasksOfDays(new DateTime(1, 1, 2), new DateTime(1, 1, 1)));
        }

        [Test]
        public void GetAllTasksDates()
        {
            var contextMock = new Mock<IContext>();
            var factoryMock = new Mock<IContextFactory>();
            var taskfilterMock = new Mock<ITaskFilter>();

            contextMock.Setup(c => c.GetUserTasksTable()).Returns(GenerateUserTaskList().AsQueryable());
            factoryMock.Setup(f => f.BuildContex()).Returns(contextMock.Object);
            taskfilterMock.Setup(t => t.Filter(It.IsAny<IQueryable<UserTask>>(), It.IsAny<FilterType>()))
                          .Returns((IQueryable<UserTask> query, FilterType filterType) => query);

            IDataModel dataModel = new DataModel(factoryMock.Object, taskfilterMock.Object);


            var dates = dataModel.GetAllTaskDates();

            Assert.AreEqual(3, dates.Count);
        }

        private List<UserTask> GenerateUserTaskList()
        {
            return new List<UserTask>
            {
                new UserTask
                {
                    Name = "TestName1",
                    Description = "TestDescription1",
                    Priority = (long)TaskPriority.Low,
                    IsNotified = false,
                    TaskDate = new DateTime(1, 1, 1),
                    NotifyDate = new DateTime(1, 1, 1)
                },
                new UserTask
                {
                    Name = "TestName2",
                    Description = "TestDescription2",
                    Priority = (long)TaskPriority.Medium,
                    IsNotified = true,
                    TaskDate = new DateTime(1, 1, 2),
                    NotifyDate = new DateTime(1, 1, 2)
                },
                new UserTask
                {
                    Name = "TestName3",
                    Description = "TestDescription3",
                    Priority = (long)TaskPriority.High,
                    IsNotified = true,
                    TaskDate = new DateTime(1, 1, 3),
                    NotifyDate = new DateTime(1, 1, 3)
                },
            };
        }

        private class DatesForTest
        {
            public static IEnumerable TestCase
            {
                get
                {
                    yield return new TestCaseData(new DateTime(1, 1, 1));
                    yield return new TestCaseData(new DateTime(1, 1, 2));
                    yield return new TestCaseData(new DateTime(1, 1, 3));
                }
            }
        }
    }
}
