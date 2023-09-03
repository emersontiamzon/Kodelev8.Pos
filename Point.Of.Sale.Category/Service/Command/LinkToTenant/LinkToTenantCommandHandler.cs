using Point.Of.Sale.Abstraction.Message;
using Point.Of.Sale.Category.Repository;
using Point.Of.Sale.Persistence.UnitOfWork;
using Point.Of.Sale.Shared.FluentResults;
using Point.Of.Sale.Shared.FluentResults.Extension;

namespace Point.Of.Sale.Category.Service.Command.LinkToTenant;

public class LinkToTenantCommandHandler : ICommandHandler<LinkToTenantCommand>
{
    private readonly IRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public LinkToTenantCommandHandler(IRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IFluentResults> Handle(LinkToTenantCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.LinkToTenant(new Shared.Models.LinkToTenant
        {
            TenantId = request.tenantId,
            EntityId = request.entityId,
        }, cancellationToken);

        if (result.IsNotFound())
        {
            return ResultsTo.NotFound().WithMessage("Category Not Found");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ResultsTo.Success();
    }
}
