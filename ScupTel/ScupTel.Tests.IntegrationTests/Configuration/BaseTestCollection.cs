using Xunit;

namespace ScupTel.Tests.IntegrationsTests.Configuration
{
    [CollectionDefinition("BaseCollection")]
    public abstract class BaseTestCollection : ICollectionFixture<BaseTestFixture>
    {
    }
}
