using Microsoft.Playwright;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;

namespace Example.Playwright.Tests;

public class ExampleTests
{
    [Test]
    public async Task ExampleDotComHasExpectedTitle()
    {
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = true,
            ExecutablePath = Environment.GetEnvironmentVariable("PLAYWRIGHT_CHROMIUM_EXECUTABLE_PATH")
        });
        var page = await browser.NewPageAsync();

        await page.GotoAsync("https://example.com");

        await Expect(page).ToHaveTitleAsync("Example Domain");
    }
}
