using Cookbook.Domain.Concrete;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cookbook.Tests
{
    public class MenuActionTest
    {
        [Fact]
        public void GetMenuActionByNameAndCheckIfItsTrue()
        {
            //Arrange
            MenuActionService menuActionService = new MenuActionService();

            //Act
            var positiveResult = menuActionService.DrawMenuViewByMenuType("MainMenu");
            var negativeResult = menuActionService.DrawMenuViewByMenuType("false");

            //Arrange
            positiveResult.Should().NotBeEmpty();
            negativeResult.Should().BeEmpty();
        }
    }
}
