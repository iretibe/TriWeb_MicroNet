//using FluentValidation;

//namespace MicroNet.Branch.Api.Configurations
//{
//    public class BranchValidator : AbstractValidator<Entities.Branch>
//    {
//        public BranchValidator()
//        {
//            RuleFor(x => x.BranchCode)
//                .NotEmpty().WithMessage("Branch code is required.")
//                .MaximumLength(50).WithMessage("Branch code must not exceed 50 characters.");

//            RuleFor(x => x.BranchName)
//                .NotEmpty().WithMessage("Branch name is required.")
//                .MaximumLength(100).WithMessage("Branch name must not exceed 100 characters.");

//            RuleFor(x => x.PhysicalAddress)
//                .NotEmpty().WithMessage("Physical address is required.")
//                .MaximumLength(250).WithMessage("Physical address must not exceed 250 characters.");

//            RuleFor(x => x.Region)
//                .NotEmpty().WithMessage("Region is required.")
//                .MaximumLength(100).WithMessage("Region must not exceed 100 characters.");

//            RuleFor(x => x.SetupDate)
//                .LessThanOrEqualTo(DateTime.Today).WithMessage("Setup date cannot be in the future.");

//            RuleFor(x => x.BranchManagerName)
//                .NotEmpty().WithMessage("Branch manager name is required.")
//                .MaximumLength(100).WithMessage("Branch manager name must not exceed 100 characters.");
//        }
//    }
//}
