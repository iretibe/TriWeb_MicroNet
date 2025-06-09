//namespace MicroNet.Branch.Api.Validations
//{
//    public class BranchTerminationValidator : IBranchTerminationValidator
//    {
//        private readonly ILoanService _loanService;
//        private readonly IClientService _clientService;
//        private readonly IFinanceService _financeService;

//        public async Task<List<string>> ValidateAsync(Guid branchId)
//        {
//            var errors = new List<string>();

//            if (await _loanService.HasActiveLoans(branchId))
//                errors.Add("Branch has active loans.");

//            if (await _clientService.HasActiveClients(branchId))
//                errors.Add("Branch has active clients.");

//            if (!await _financeService.IsGLBalanced(branchId))
//                errors.Add("GL is not reconciled.");

//            return errors;
//        }
//    }
//}
