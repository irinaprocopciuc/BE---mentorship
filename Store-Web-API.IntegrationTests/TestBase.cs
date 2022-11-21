using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Store_Web_API.IntegrationTests
{
    public class TestBase : IClassFixture<AppFixture>, IAsyncLifetime
    {
        protected readonly AppFixture Fixture;

        public TestBase(AppFixture fixture)
        {
            this.Fixture = fixture;
        }

        public async Task DisposeAsync()
        {
            //await Fixture.DeleteDB();
        }

        public async Task InitializeAsync()
        {
            await Fixture.ResetState();
        }
    }
}
