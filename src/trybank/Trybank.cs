namespace Trybank.Lib;

public class TrybankLib
{
    public bool Logged;
    public int loggedUser;

    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;

    public TrybankLib()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    // 1. Construa a funcionalidade de cadastrar novas contas
    public void RegisterAccount(int number, int agency, int pass)
    {
        String newNumberAgencyConcat = number.ToString() + agency.ToString();

        for (int i = 0; i < Bank.Length; i += 1)
        {
            String registeredNumberAgencyConcat = Bank[i, 0].ToString() + Bank[i, 1].ToString();

            if (registeredNumberAgencyConcat == newNumberAgencyConcat)
            {
                throw new ArgumentException("A conta já está sendo usada!");
            }
            else if (Bank[i, 0] == 0)
            {
                Bank[i, 0] = number;
                Bank[i, 1] = agency;
                Bank[i, 2] = pass;
                Bank[i, 3] = 0;
                registeredAccounts += 1;
                break;
            }
        }
    }

    // 2. Construa a funcionalidade de fazer Login
    public void Login(int number, int agency, int pass)
    {
        if (Logged) throw new AccessViolationException("Usuário já está logado");
        String loginNumberAgencyConcat = number.ToString() + agency.ToString();

        for (int i = 0; i < Bank.Length; i += 1)
        {
            String registeredNumberAgencyConcat = Bank[i, 0].ToString() + Bank[i, 1].ToString();
            int registeredPass = Bank[i, 2];

            if (registeredNumberAgencyConcat == loginNumberAgencyConcat && pass == registeredPass)
            {
                Logged = true;
                loggedUser = i;
                break;
            }
            else if (registeredNumberAgencyConcat == loginNumberAgencyConcat && pass != registeredPass)
            {
                throw new ArgumentException("Senha incorreta");
            }
            else if (Bank[i, 0] == 0) throw new ArgumentException("Agência + Conta não encontrada");
        }

    }

    // 3. Construa a funcionalidade de fazer Logout
    public void Logout()
    {
        IsLoggedIn();
        Logged = false;
        loggedUser = -99;
    }

    // 4. Construa a funcionalidade de checar o saldo
    public int CheckBalance()
    {
        IsLoggedIn();
        return Bank[loggedUser, 3];
    }

    // 5. Construa a funcionalidade de depositar dinheiro
    public void Deposit(int value)
    {
        IsLoggedIn();
        Bank[loggedUser, 3] += value;
    }

    // 6. Construa a funcionalidade de sacar dinheiro
    public void Withdraw(int value)
    {
        IsLoggedIn();
        int currentBalance = Bank[loggedUser, 3];
        if (value > currentBalance) throw new InvalidOperationException("Saldo insuficiente");
        Bank[loggedUser, 3] -= value;
    }

    // 7. Construa a funcionalidade de transferir dinheiro entre contas
    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        throw new NotImplementedException();
    }

    public void IsLoggedIn()
    {
        if (!Logged) throw new AccessViolationException("Usuário não está logado");
    }


}
