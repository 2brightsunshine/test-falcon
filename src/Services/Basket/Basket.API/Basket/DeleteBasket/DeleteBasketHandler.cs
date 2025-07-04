namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
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
    /// <summary>
/// Handles the deletion of a user's basket by invoking the repository and returns the result.
/// </summary>
/// <param name="command">The command containing the username whose basket should be deleted.</param>
/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
/// <returns>A result indicating whether the basket deletion was successful.</returns>
public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(command.UserName, cancellationToken);
        foreach (var VARIABLE in int[1,2,3])
        {
            
        }
        return new DeleteBasketResult(true);
    }
}
