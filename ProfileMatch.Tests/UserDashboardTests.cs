using Bunit;
using Bunit.TestDoubles;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

using Moq;

using ProfileMatch.Components.User;
using ProfileMatch.Services;
using ProfileMatch.Sites;

using Xunit;

namespace ProfileMatch.Tests
{
    public class UserDashboardTests
    {
        readonly TestContext ctx = new();
        private readonly Mock<IStringLocalizer<LanguageService>> _stringLocalizerMock;
        readonly string textValue = "!localized!";
        public UserDashboardTests()
        {
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            _stringLocalizerMock = new Mock<IStringLocalizer<LanguageService>>();
            _stringLocalizerMock.Setup(l => l[It.IsAny<string>()]).Returns(new LocalizedString("name", textValue));
            ctx.Services.AddSingleton(_stringLocalizerMock.Object);

        }
        //[Fact]
        public void Renders_UserDashboard()
        {
            // Act
            var cut = ctx.RenderComponent<UserDashboard>();
      

            // Assert
           Assert.NotNull(cut);
        }
    }
}