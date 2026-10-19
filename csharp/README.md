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
dotnet test Example.Playwright.Tests --no-restore --filter FullyQualifiedName~ExampleTests
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

## VSDRIS login test

`VsdrisLoginTests.cs` opens the VSDRIS test login page, selects the VATARAS
provider, switches to `Slaptažodis`, enters credentials, and submits the form.
Set `VSDRIS_USERNAME` and `VSDRIS_PASSWORD` in the test process environment
using your local secure input or CI secrets. No credentials are stored in code.

After submitting, the test waits up to 30 seconds for the exact authenticated
URL `https://testvsdris.vva.lt/savitarna/#/pagrindinis`, including the hash route.

The submit button is assumed to be named `Prisijungti`; set `VSDRIS_LOGIN_BUTTON`
if its accessible name differs. The input locators expect associated labels
`Naudotojo vardas` and `Slaptažodis` within `#LogonTabs-2`.

```sh
dotnet test Example.Playwright.Tests --filter FullyQualifiedName~VsdrisLoginTests
```

Cloud network settings must permit `testvsdris.vva.lt` and any external identity
provider hosts to which it redirects. Form submission requires actual process
environment credentials; proxy-only credential placeholders cannot fill a form.

## Run VSDRIS locally on Windows

Install Git, the .NET 8 SDK, and PowerShell 7 (`pwsh`). Connect GlobalProtect
and confirm the login page opens in your normal browser. In PowerShell 7:

```powershell
git clone https://github.com/KristRizTieto/TEST.git
cd TEST
dotnet build csharp/Example.Playwright.Tests
pwsh csharp/Example.Playwright.Tests/bin/Debug/net8.0/playwright.ps1 install chromium

$credential = Get-Credential -Message 'Enter your VSDRIS credentials'
$env:VSDRIS_USERNAME = $credential.UserName
$env:VSDRIS_PASSWORD = $credential.GetNetworkCredential().Password
try {
    dotnet test csharp/Example.Playwright.Tests --filter FullyQualifiedName~VsdrisLoginTests
} finally {
    Remove-Item Env:VSDRIS_USERNAME, Env:VSDRIS_PASSWORD -ErrorAction SilentlyContinue
    Remove-Variable credential
}
```

If you already cloned the repository, open that folder and run `git pull`
instead of cloning again. Do not source `environment.sh` on Windows; it is
specific to the cloud Linux machine. The password prompt keeps credentials out
of your command history. The test runs headlessly unless you change `Headless`
to `false` in `VsdrisLoginTests.cs`. Actual login and the supplied input labels
still need to be verified on your VPN-connected machine.
