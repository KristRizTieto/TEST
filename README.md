# Login end-to-end test

For the separate **C# Playwright example.com title test**, see [csharp/README.md](csharp/README.md).

This test targets the public OrangeHRM demo at
https://opensource-demo.orangehrmlive.com/web/index.php/auth/login.

Install Node.js 20 or later, then dependencies and Chromium:

```sh
npm ci
npx playwright install chromium
```

To use an existing Chromium installation instead of downloading one, set
`PLAYWRIGHT_CHROMIUM_EXECUTABLE_PATH` to its executable path.

Provide valid demo credentials as `LOGIN_USERNAME` and `LOGIN_PASSWORD` through
your shell or CI secret settings, then run:

```sh
npm run test:e2e
```

Credentials are read from the process environment and have no fallback values.
Missing required variables fail the test. `.env` files are ignored by Git but
are not automatically loaded.

The test fills the `Username` and `Password` inputs, clicks `Login`, and waits
for the `Dashboard` heading. Set `LOGIN_URL` to override the public demo URL;
other applications may require changes to these locators.

Traces, screenshots, and videos are disabled to avoid retaining login artifacts.
