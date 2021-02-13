using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace HybridRequests.Unit.Tests
{
    public class PropertyInfoExtensionsUnitTests
    {
        [Theory, AutoDataAttribute]
        public void Should_Force_State_Value_From_Object(Person person, string name)
        {
            var property = person.GetType().GetProperties().FirstOrDefault();

            property.ForceStateValue(person, name);

            person.Name.Should().Be(name);
        }
    }
}