using Microsoft.Playwright;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;

namespace Example.Playwright.Tests;

public class VsdrisLoginTests
{
    private static string RequiredEnvironmentVariable(string name) =>
        Environment.GetEnvironmentVariable(name) is { Length: > 0 } value
            ? value
            : throw new InvalidOperationException($"Set {name} in the test process environment.");

    [Test]
    [Category("Login")]
    public async Task PasswordLoginOpensAuthenticatedPage()
    {
        var username = RequiredEnvironmentVariable("VSDRIS_USERNAME");
        var password = RequiredEnvironmentVariable("VSDRIS_PASSWORD");

        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = true,
            ExecutablePath = Environment.GetEnvironmentVariable("PLAYWRIGHT_CHROMIUM_EXECUTABLE_PATH")
        });
        // Do not record login traces, video, or authenticated session state.
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://testvsdris.vva.lt/savitarna/Login");

        await page.Locator("a[href='/savitarna/Account/ExternalLogin?provider=VATARAS']").ClickAsync();
        await page.Locator("a.t-link[href='#LogonTabs-2']").ClickAsync();

        // Restrict the fields to the selected password tab.
        var passwordTab = page.Locator("#LogonTabs-2");
        await Expect(passwordTab).ToBeVisibleAsync();
        await passwordTab.GetByLabel("Naudotojo vardas", new() { Exact = true }).FillAsync(username);
        await passwordTab.GetByLabel("Slaptažodis", new() { Exact = true }).FillAsync(password);
        await passwordTab.GetByRole(AriaRole.Button, new()
        {
            Name = Environment.GetEnvironmentVariable("VSDRIS_LOGIN_BUTTON") ?? "Prisijungti",
            Exact = true
        }).ClickAsync();

        await Expect(page).ToHaveURLAsync(
            "https://testvsdris.vva.lt/savitarna/#/pagrindinis",
            new() { Timeout = 30_000 });
    }
}
