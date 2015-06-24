using System;
using System.Linq;
using System.Reflection;
using BusinessSafe.Domain.Common;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities
{
    [TestFixture]
    [Category("Unit")]
    public class EnsurePublicMethodsAreMarkedAsVirtualTests
    {
        [Test]
        public void All_public_methods_should_be_marked_as_public()
        {
            //Given
            //When
            //Then
            var type = typeof(IAuditable);
            var types = AppDomain
                            .CurrentDomain
                            .GetAssemblies()
                            .ToList()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => type.IsAssignableFrom(p));

            foreach (var domainEntity in types)
            {
                if (domainEntity.Assembly.FullName.Contains("Test"))
                {
                    continue; // Don't test test classes
                }
                    
                
                if (domainEntity.IsAbstract == false) // Abstract class issues picked up by the child classes
                {
                    PropertyInfo[] virtualProperties = domainEntity
                        .GetProperties()
                        .Where(p => p.GetGetMethod().IsVirtual).ToArray();

                    PropertyInfo[] nonVirtualProperties = domainEntity
                        .GetProperties()
                        .ToArray();

                    var methodPropertiesResultDifference = nonVirtualProperties.Except(virtualProperties);

                    Assert.That(virtualProperties.Length, Is.EqualTo(nonVirtualProperties.Length),
                                string.Format("Type {0} has not got properties marked as virtual", domainEntity.Name));

                    var virtualMethods = domainEntity
                        .GetMethods()
                        .Where(x => x.IsPublic)
                        .Where(x => x.IsStatic == false)
                        .Where(x => x.IsAbstract == false)
                        .Where(x => x.IsConstructor == false)
                        .Where(x => x.Name.Contains("GetType") == false)
                        .Where(p => p.IsVirtual).ToArray();

                    var nonVirtualMethods = domainEntity
                        .GetMethods()
                        .Where(x => x.IsPublic)
                        .Where(x => x.IsAbstract == false)
                        .Where(x => x.IsStatic == false)
                        .Where(x => x.IsConstructor == false)
                        .Where(x => x.Name.Contains("GetType") == false)
                        .ToArray();

                    var methodResultDifference = nonVirtualMethods.Except(virtualMethods);
                    
                    Assert.That(virtualMethods.Length, Is.EqualTo(nonVirtualMethods.Length),
                                string.Format("Type {0} has not got methods marked as virtual.", domainEntity.Name));
                }
            }
            
            
        }
    }
}