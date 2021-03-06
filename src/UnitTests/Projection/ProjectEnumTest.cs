﻿namespace AutoMapper.UnitTests.Projection
{
    using AutoMapper.QueryableExtensions;
    using Should;
    using System.Linq;
    using Xunit;

    public class ProjectEnumTest
    {
        public ProjectEnumTest()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerType, string>().ProjectUsing(ct => ct.ToString().ToUpper());
        }

        [Fact]
        public void ProjectingEnumToString()
        {
            var customers = new[] { new Customer() { FirstName = "Bill", LastName = "White", CustomerType = CustomerType.Vip } }.AsQueryable();

            var projected = customers.Project().To<CustomerDto>();
            projected.ShouldNotBeNull();
            Assert.Equal(customers.Single().CustomerType.ToString().ToUpper(), projected.Single().CustomerType);
        }

        public class Customer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public CustomerType CustomerType { get; set; }
        }

        public class CustomerDto
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string CustomerType { get; set; }
        }

        public enum CustomerType
        {
            Regular,
            Vip,
        }
    }
}
