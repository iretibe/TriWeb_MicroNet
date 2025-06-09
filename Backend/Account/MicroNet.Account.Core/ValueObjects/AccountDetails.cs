namespace MicroNet.Account.Core.ValueObjects
{
    public class AccountDetails
    {
        public string AccountNumber { get; private set; }
        public string AccountName { get; private set; }
        public decimal Balance { get; private set; }
        public string BranchName { get; private set; }

        private AccountDetails() { }

        public AccountDetails(string number, string name, decimal balance, string branchName)
        {
            AccountNumber = number;
            AccountName = name;
            Balance = balance;
            BranchName = branchName;
        }
    }
}
