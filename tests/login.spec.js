const { test, expect } = require('@playwright/test');

function requiredEnv(name) {
  const value = process.env[name];
  if (!value) throw new Error(`Set ${name} in the test process environment.`);
  return value;
}

test('valid credentials open the dashboard', async ({ page }) => {
  const loginUrl = process.env.LOGIN_URL || 'https://opensource-demo.orangehrmlive.com/web/index.php/auth/login';
  const username = requiredEnv('LOGIN_USERNAME');
  const password = requiredEnv('LOGIN_PASSWORD');

  await page.goto(loginUrl);
  await page.getByPlaceholder('Username', { exact: true }).fill(username);
  await page.getByPlaceholder('Password', { exact: true }).fill(password);
  await page.getByRole('button', {
    name: 'Login',
    exact: true,
  }).click();

  await expect(page.getByRole('heading', {
    name: 'Dashboard',
    exact: true,
  })).toBeVisible();
});
