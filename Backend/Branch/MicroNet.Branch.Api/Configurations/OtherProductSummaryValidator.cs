//using FluentValidation;
//using MicroNet.Branch.Api.Entities;

//namespace MicroNet.Branch.Api.Configurations
//{
//    public class OtherProductSummaryValidator : AbstractValidator<OtherProductSummary>
//    {
//        public OtherProductSummaryValidator()
//        {
//            RuleFor(x => x.BranchId)
//                .NotEmpty().WithMessage("Branch ID is required.");

//            RuleFor(x => x.ProductType)
//                .NotEmpty().WithMessage("Product type is required.")
//                .MaximumLength(100).WithMessage("Product type must not exceed 100 characters.");

//            RuleFor(x => x.NumberOfLoans)
//                .GreaterThanOrEqualTo(0).WithMessage("Number of loans must be 0 or greater.");

//            RuleForEach(x => new[]
//            {
//            x.TotalLoanAmount,
//            x.TotalInterest,
//            x.TotalRepayment,
//            x.ProcessingFees,
//            x.PenaltyCharges,
//            x.TotalLoanBalance,
//            x.ProductAmount,
//            x.Interest,
//            x.Withdrawal,
//            x.ManagementFees,
//            x.Balance
//        }).Must(amount => amount >= 0).WithMessage("Amount fields must be 0 or greater.");
//        }
//    }
//}
