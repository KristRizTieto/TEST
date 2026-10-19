const { defineConfig } = require('@playwright/test');

module.exports = defineConfig({
  testDir: './tests',
  use: {
    browserName: 'chromium',
    launchOptions: {
      executablePath: process.env.PLAYWRIGHT_CHROMIUM_EXECUTABLE_PATH || undefined,
    },
    // Login artifacts can contain credentials or authenticated session data.
    trace: 'off',
    screenshot: 'off',
    video: 'off',
  },
});
