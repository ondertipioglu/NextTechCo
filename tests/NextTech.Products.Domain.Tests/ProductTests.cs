using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NextTech.Products.Domain;
using FluentAssertions;
using NextTech.Products.Domain.SeedWork;
using AutoFixture;

namespace NextTech.Products.Domain.Tests
{
    public class ProductTests
    {
        private IFixture fixture;
        public ProductTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void Given_Product_Has_Valid_Parameter_Then_Should_Be_Create()
        {
            DateTime createdOn = DateTime.Now;
            string title = "Product 1 Title";
            string description = "Product 1 Description";
            int stock = 12;
            bool active = true;

            var actual = Domain.Product.Create(createdOn, title, description, stock, active);

            actual.CreatedOn.Should().Be(createdOn);
            actual.Title.Should().Be(title);
            actual.Description.Should().Be(description);
            actual.Stock.Should().Be(stock);
            actual.Active.Should().Be(active);
        }

        [Fact]
        public void Given_Title_Is_Empty_Then_Should_ThrowException()
        {
            Action act = () => Domain.Product.Create(DateTime.Now, String.Empty, "Product 1 Description", 120, true);

            act.Should().ThrowExactly<BusinessRuleValidationException>();
        }

        [Fact]
        public void Given_Title_Has_Greater200Character_Then_Should_ThrowException()
        {
            var title = fixture.GetStringOfLength(250);

            Action act = () => Domain.Product.Create(DateTime.Now, title, "Product 1 Description", 120, true);

            act.Should().ThrowExactly<BusinessRuleValidationException>();
        }

        [Fact]
        public void Apply_Product_Enable_Should_Valid_Behavior()
        {
            DateTime createdOn = DateTime.Now;
            string title = "Product 1 Title";
            string description = "Product 1 Description";
            int stock = 12;
            bool active = false;

            var actual = Domain.Product.Create(createdOn, title, description, stock, active);

            actual.Enable();

            actual.Active.Should().Be(true);
        }

        [Fact]
        public void When_Product_Active_Apply_Product_Enable_Then_Should_ThrowException()
        {
            DateTime createdOn = DateTime.Now;
            string title = "Product 1 Title";
            string description = "Product 1 Description";
            int stock = 12;
            bool active = true;

            Action act = () =>
            {
                var actual = Domain.Product.Create(createdOn, title, description, stock, active);
                actual.Enable();
            };

            act.Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void Apply_Product_Disable_Should_Valid_Behavior()
        {
            DateTime createdOn = DateTime.Now;
            string title = "Product 1 Title";
            string description = "Product 1 Description";
            int stock = 12;
            bool active = true;

            var actual = Domain.Product.Create(createdOn, title, description, stock, active);

            actual.Disable();

            actual.Active.Should().Be(false);
        }

        [Fact]
        public void When_Product_Passive_Apply_Product_Passive_Then_Should_ThrowException()
        {
            DateTime createdOn = DateTime.Now;
            string title = "Product 1 Title";
            string description = "Product 1 Description";
            int stock = 12;
            bool active = false;

            Action act = () =>
            {
                var actual = Domain.Product.Create(createdOn, title, description, stock, active);
                actual.Disable();
            };

            act.Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void Given_Product_Title_And_Description_Change_Behavior()
        {
            var expectedTitle = "exceptedTitle";
            var expectedDescription = "exceptedDescription";

            DateTime createdOn = DateTime.Now;
            string title = "Product 1 Title";
            string description = "Product 1 Description";
            int stock = 12;
            bool active = true;

            var actual = Domain.Product.Create(createdOn, title, description, stock, active);
            actual.ChangeTitleAndDescriptionInfo(expectedTitle, expectedDescription);
         
            actual.Title.Should().Be(expectedTitle);
            actual.Description.Should().Be(expectedDescription);           
        }
    }

}
