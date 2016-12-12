#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion
using DataAccess;
using DataAccess.Entity;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq.Protected;

namespace DataAccessTests.Repository
{
    [TestFixture]
    public class GenericRepositoryShould
    {
        private Mock<IDataContext> _dataContextMock;

        private Mock<DbSet<TestEntity>> _testEntityDbSetMock;

        private Mock<GenericRepository<TestEntity>> _genericRepositoryPartial;

        [SetUp]
        public void SetUp()
        {
            _dataContextMock = new Mock<IDataContext>();
            _testEntityDbSetMock = new Mock<DbSet<TestEntity>>();
            _dataContextMock.Setup(x => x.Set<TestEntity>()).Returns(_testEntityDbSetMock.Object);
            _genericRepositoryPartial = new Mock<GenericRepository<TestEntity>>(_dataContextMock.Object);
            _genericRepositoryPartial.CallBase = true;
        }

        [Test]
        public void DoGetAnEntityById()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var testEntities = new List<TestEntity>
            {
                new TestEntity { Id = id },
                new TestEntity { Id = Guid.NewGuid() },
                new TestEntity { Id = Guid.NewGuid() }
            };

            _genericRepositoryPartial.DefaultValue = DefaultValue.Mock;
            _genericRepositoryPartial.Protected().Setup<TestEntity>("Find", id).Returns(testEntities.Single(x => x.Id == id));

            //Act
            var result = _genericRepositoryPartial.Object.GetByID(id);

            //Assert
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void DoInsertANewItem()
        {
            //Arrange
            var testId = Guid.NewGuid();
            var testEntity = new TestEntity { Id = testId };

            //Act
            _genericRepositoryPartial.Object.Insert(testEntity);

            //Assert
            _testEntityDbSetMock.Verify(x => x.Add(testEntity));
        }

        [Test]
        public void DoInsertANewEntity()
        {
            //Arrange
            var newEntity = new TestEntity
            {
                SampleProperty = "Some value"
            };

            //Act
            _genericRepositoryPartial.Object.Insert(newEntity);

            //Assert
            _testEntityDbSetMock.Verify(x => x.Add(newEntity));
        }

        [Test]
        public void DoDeleteAnExistingEntity()
        {
            //Arrange
            var existingEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                SampleProperty = "Some value"
            };

            _genericRepositoryPartial.Setup(x => x.GetByID(existingEntity.Id)).Returns(existingEntity);
            _genericRepositoryPartial.Setup(x => x.Delete(existingEntity));

            //Act
            _genericRepositoryPartial.Object.Delete(existingEntity.Id);

            //Assert
            _genericRepositoryPartial.Verify(x => x.Delete(existingEntity));
        }
    }

    public class TestEntity : IHasIdentityEntity
    {
        public Guid Id { get; set; }

        public string SampleProperty { get; set; }
    }
}
