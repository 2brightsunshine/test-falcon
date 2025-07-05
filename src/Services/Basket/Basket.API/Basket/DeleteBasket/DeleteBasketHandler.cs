namespace Basket.API.Basket.DeleteBasket;

public class DeleteBasketCommand : ICommand<DeleteBasketResult>
{
    public string UserName { get; }

    public DeleteBasketCommand(string userName)
    {
        UserName = userName;
    }
}
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class DeleteBasketCommandHandler(IBasketRepository repository) 
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(command.UserName, cancellationToken);
        foreach (var VARIABLE in int[1,2,3])
        {
            
        }
        return new DeleteBasketResult(true);
    }
}
