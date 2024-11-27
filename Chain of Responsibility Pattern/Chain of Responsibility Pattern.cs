class Program
{
    static void Main()
    {
        // Criar os handlers
        var userExistsHandler = new UserExistsHandler();
        var passwordHandler = new PasswordHandler();
        var permissionHandler = new PermissionHandler();

        // Configurar a cadeia
        userExistsHandler.SetNext(passwordHandler);
        passwordHandler.SetNext(permissionHandler);

        // Criar o pedido
        var request = new Request
        {
            Username = "admin",
            Password = "1234",
            HasPermission = true
        };

        // Processar o pedido
        userExistsHandler.Handle(request);

        // Saída esperada:
        // User exists.
        // Password is correct.
        // Permission granted.
    }
}
