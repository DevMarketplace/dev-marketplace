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
using BusinessLogic.Utilities;
using NUnit.Framework;

namespace BusinessLogicTests.Utilities
{

    class SomeSource
    {
        public string SomeProp1 { get; set; }

        public string SameNameDifferentType { get; set; }

        public object SomeComplexType { get; set; }

        public int SamePropNamePrimitiveType { get; set; }

        public decimal SamePropNameNotPrimitiveValueType { get; set; }

        public string SamePropNameReferenceType { get; set; }
    }

    class SomeTarget
    {
        public string DbProp1 { get; set; }

        public int SameNameDifferentType { get; set; }

        public object SomeComplexType { get; set; }

        public int SamePropNamePrimitiveType { get; set; }

        public decimal SamePropNameNotPrimitiveValueType { get; set; }

        public string SamePropNameReferenceType { get; set; }
    }

    [TestFixture]
    public class SimplePropertyMapperShould
    {
        [Test]
        public void DoNotCopyPropertiesIfNamesMismatch()
        {
            //Arrange
            var source = new SomeSource
            {
                SomeProp1 = "Test String"
            };

            var target = new SomeTarget();

            //Act
            SimplePropertyMapper.Map(source, target);

            //Assert
            Assert.AreNotEqual(source.SomeProp1, target.DbProp1);
        }

        [Test]
        public void DoNotCopyPropertiesIfTypesMismatch()
        {
            //Arrange
            var source = new SomeSource
            {
                SameNameDifferentType = "Test String"
            };

            var target = new SomeTarget();

            //Act
            SimplePropertyMapper.Map(source, target);

            //Assert
            Assert.AreNotEqual(source.SameNameDifferentType, target.SameNameDifferentType);
        }

        [Test]
        public void DoNotCopyPropertiesIfTypesAreNotPrimitiveOrInTheAllowedTypesList()
        {
            //Arrange
            var source = new SomeSource
            {
                SomeComplexType = (object)("TEST complex type")
            };

            var target = new SomeTarget();

            //Act
            SimplePropertyMapper.Map(source, target);

            //Assert
            Assert.AreNotEqual(source.SomeComplexType, target.SomeComplexType);
        }

        [Test]
        public void CopyPropertiesIfNameTypeAndPrimitivityMatches()
        {
            //Arrange
            var source = new SomeSource
            {
                SamePropNamePrimitiveType = 1
            };
            var target = new SomeTarget();

            //Act
            SimplePropertyMapper.Map(source, target);

            //Assert
            Assert.AreEqual(source.SamePropNamePrimitiveType, target.SamePropNamePrimitiveType);
        }

        [Test]
        public void CopyPropertiesIfNameTypeAndValueTypeMatches()
        {
            //Arrange
            var source = new SomeSource
            {
                SamePropNameNotPrimitiveValueType = (decimal)1.2
            };
            var target = new SomeTarget();

            //Act
            SimplePropertyMapper.Map(source, target);

            //Assert
            Assert.AreEqual(source.SamePropNameNotPrimitiveValueType, target.SamePropNameNotPrimitiveValueType);
        }

    }
}
