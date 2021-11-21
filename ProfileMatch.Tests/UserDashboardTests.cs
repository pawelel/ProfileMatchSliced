using Bunit;

using ProfileMatch.Sites;

using Xunit;

namespace ProfileMatch.Tests
{
    public class UserDashboardTests : TestContext
    {
        [Fact]
        public void Test1()
        {
            var cut = RenderComponent<UserDashboard>();
            cut.Find("");
        }
    }
}