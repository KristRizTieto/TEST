# C# Playwright test

Requires the .NET 8 SDK. From this directory:

```sh
dotnet restore Example.Playwright.Tests
dotnet build Example.Playwright.Tests --no-restore
```

Install Chromium using the generated Playwright script (requires PowerShell):

```sh
pwsh Example.Playwright.Tests/bin/Debug/net8.0/playwright.ps1 install chromium
```

Alternatively, use an existing Chromium installation on Linux:

```sh
export PLAYWRIGHT_CHROMIUM_EXECUTABLE_PATH=/usr/bin/chromium
```

Run the test:

```sh
dotnet test Example.Playwright.Tests --no-restore
```

The test opens `https://example.com` in headless Chromium and asserts that the
page title is `Example Domain`. It requires network access to example.com.

In this cloud environment, if .NET is not already on PATH, activate the local SDK:

```sh
source /workspace/TEST/csharp/environment.sh
```

This also selects the installed system Chromium and writable .NET/NuGet runtime
directories. If navigation fails with `ERR_TUNNEL_CONNECTION_FAILED`, allow
`example.com` in the cloud environment's network settings and apply the change.

To see the browser on a computer with a graphical desktop, change `Headless`
to `false` in `Example.Playwright.Tests/ExampleTests.cs`. The cloud shell alone
does not provide a graphical display.
