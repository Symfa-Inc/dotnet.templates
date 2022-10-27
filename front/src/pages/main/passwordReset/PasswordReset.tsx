export function PasswordReset() {
  return (
    <div>
      <h1>Reset Password</h1>
      <p>Enter your email address below and we will send you a link to reset your password.</p>
      <form>
        <input type="email" name="email" id="email" placeholder="email" />
        <button type="submit">Reset Password</button>
      </form>
    </div>
  );
}
