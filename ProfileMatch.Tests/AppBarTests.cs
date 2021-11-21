// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bunit;
using FluentAssertions;

using MudBlazor;

using Xunit;

using static Bunit.ComponentParameterFactory;

namespace ProfileMatch.Tests
{
    public class AppBarTests : TestContext
    {
        /// <summary>
        /// AppBar with modified Toolbar class
        /// </summary>
        [Fact]
        public void AppBarWithModifiedToolBarClass()
        {
            var comp = RenderComponent<MudAppBar>(Parameter(nameof(MudAppBar.ToolBarClass), "test-class"));

            // Find the Toolbar inside the AppBar
            comp.Find("div").ToMarkup()
                .Should()
                .Contain("test-class");
        }

    }
}
